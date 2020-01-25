using Tss.Core.Entities;
using System;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;
using Ardalis.GuardClauses;

namespace Tss.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<AttributeValue> _attributeValueRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IAuthHelper _authHelper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IRepository<Category> categoryRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IRepository<Product> productRepo,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _categoryRepo = categoryRepo ?? throw new ArgumentNullException(nameof(categoryRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new ArgumentNullException(nameof(attributeValueRepo));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        public async Task<ProductFilterResponse> GetFilters()
        {
            List<string> attributes = new List<string>{"Color", "Size"};
            List<AttributeValue> attributeValues = await _attributeValueRepo.Get(new GetAttributeValueByNamesSpecification(attributes, _authHelper));
            List<ProductFilterCategoryResponse> categories = await _categoryRepo.Get(new GetCategorySelectListSpecification());
            return new ProductFilterResponse(attributeValues, categories);
        }

        public async Task<List<Product>> Get()
        {
            return await _productRepo.Get(new GetProductSpecification(_authHelper));
        }

        public async Task<Product> GetOne(int productId)
        {
            return await _productRepo.GetOne(new GetProductByIdSpecification(productId, _authHelper));
        }

        public async Task<Product> Create(ProductCreateRequest input)
        {
            Product product = await _productRepo.GetOne(new GetProductByNameSpecification(input.Name, _authHelper));
            Guard.Against.AlreadyExist(product, nameof(Product));    

            product = new Product(input);
            _productRepo.Post(product);
            await _unitOfWork.SaveAsync();
            return product;
        }

        public async Task<Product> Update(Product input)
        {
            Product product = await _productRepo.Get(input.ProductId);
            Guard.Against.NotFound(product, nameof(Product));

            _productRepo.Put(product);
            await _unitOfWork.SaveAsync();
            return product;
        }

        public async Task<Product> Delete(int ProductId)
        {
            Product product = await _productRepo.Get(ProductId);
            Guard.Against.NotFound(product, nameof(Product));

            _productRepo.Delete(product);
            await _unitOfWork.SaveAsync();
            return product;
        }
    }
}