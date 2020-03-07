using Tss.Core.Entities;
using System;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Tss.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<AttributeValue> _attributeValueRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<ProductAttribute> _productAttributeRepo;
        private readonly IRepository<ProductCategory> _productCategoryRepo;
        private readonly IRepository<ShoppingCart> _shoppingCartRepo;
        private readonly IAuthHelper _authHelper;
        private readonly IMediaHelper _mediaHelper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IRepository<Category> categoryRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IRepository<Product> productRepo,
            IRepository<ProductAttribute> productAttributeRepo,
            IRepository<ProductCategory> productCategoryRepo,
            IRepository<ShoppingCart> shoppingCartRepo,
            IAuthHelper authHelper,
            IMediaHelper mediaHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _categoryRepo = categoryRepo ?? throw new ArgumentNullException(nameof(categoryRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new ArgumentNullException(nameof(attributeValueRepo));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _productAttributeRepo = productAttributeRepo ?? throw new ArgumentNullException(nameof(productAttributeRepo));
            _productCategoryRepo = productCategoryRepo ?? throw new ArgumentNullException(nameof(productCategoryRepo));
            _shoppingCartRepo = shoppingCartRepo ?? throw new ArgumentNullException(nameof(shoppingCartRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
            _mediaHelper = mediaHelper ?? throw new System.ArgumentNullException(nameof(mediaHelper));
        }

        public async Task<ProductFilterResponse> GetFilters()
        {
            List<string> attributes = new List<string> { "Color", "Size" };
            List<AttributeValue> attributeValues = await _attributeValueRepo.Get(new GetAttributeValueByNamesSpecification(attributes, ProductAttribute.AllowedValues, _authHelper));
            List<ProductFilterCategoryResponse> categories = await _categoryRepo.Get(new GetCategorySelectListSpecification());
            double maxPrice = await _productRepo.GetMax(p => p.Price);
            double minPrice = await _productRepo.GetMin(p => p.Price);
            return new ProductFilterResponse(attributeValues, categories, maxPrice, minPrice);
        }

        public async Task<PaginatedList<Product>> Get(
            ProductSearchRequest input
        )
        {
            return await _productRepo.GetPaginated(new GetProductSpecification(
                _authHelper,
                input
            ));
        }

        public async Task<ProductResponse> GetOne(int productId)
        {
            return new ProductResponse(await _productRepo.GetOne(new GetProductByIdSpecification(productId, _authHelper)));
        }

        public async Task<ProductResponse> Create(ProductCreateRequest input)
        {
            Product product = await _productRepo.GetOne(new GetProductByNameSpecification(input.Name, _authHelper));
            Guard.Against.AlreadyExist(product, nameof(product));

            List<AttributeValue> attributeValues = await _attributeValueRepo.Get(new GetAttributeValueByIdsSpecification(input.Attributes, ProductAttribute.AllowedValues, _authHelper));
            Guard.Against.NotFound(attributeValues, nameof(AttributeValue));

            Category category = await _categoryRepo.Get(input.CategoryId);
            Guard.Against.NotFound(category, nameof(Category));

            product = new Product(input);
            _productRepo.Post(product);
            await _unitOfWork.SaveAsync();

            product.Create(input, attributeValues, category);
            await _unitOfWork.SaveAsync();
            return new ProductResponse(product);
        }

        public async Task<ProductResponse> Update(ProductUpdateRequest input)
        {
            Product product = await _productRepo.GetOne(new GetProductByIdSpecification(input.ProductId, _authHelper));
            Guard.Against.NotFound(product, nameof(Product));

            List<AttributeValue> attributeValues = await _attributeValueRepo.Get(new GetAttributeValueByIdsSpecification(input.Attributes, ProductAttribute.AllowedValues, _authHelper));
            Guard.Against.NotFound(attributeValues, nameof(AttributeValue));

            Category category = await _categoryRepo.Get(input.CategoryId);
            Guard.Against.NotFound(category, nameof(Category));

            await product.Update(input, attributeValues, category, _mediaHelper);
            _productRepo.Put(product);
            await _unitOfWork.SaveAsync();

            _productAttributeRepo.DeleteRange(product.ProductAttribute.ToList());
            _productCategoryRepo.DeleteRange(product.ProductCategory.ToList());
            await _unitOfWork.SaveAsync();

            product.UpdateAttributes(input);
            await _unitOfWork.SaveAsync();

            return new ProductResponse(product, true);
        }

        public async Task<ProductResponse> Delete(int productId)
        {
            Product product = await _productRepo.GetOne(new GetProductByIdSpecification(productId, _authHelper));
            Guard.Against.NotFound(product, nameof(Product));

            await product.Remove(_mediaHelper);
            // _shoppingCartRepo.DeleteRange(product.ShoppingCart.ToList());
            // await _unitOfWork.SaveAsync();

            _productRepo.Delete(product);
            await _unitOfWork.SaveAsync();
            return new ProductResponse(product);
        }
    }
}