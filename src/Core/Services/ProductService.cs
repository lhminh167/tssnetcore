using Tss.Core.Entities;
using System;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;
using Ardalis.GuardClauses;
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
        private readonly IRepository<OrderDetail> _orderDetailRepo;
        private readonly IRepository<Review> _reviewRepo;
        private readonly IAuthHelper _authHelper;
        private readonly IMediaHelper _mediaHelper;
        private readonly IProductHelper _productHelper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IRepository<Category> categoryRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IRepository<Product> productRepo,
            IRepository<ProductAttribute> productAttributeRepo,
            IRepository<ProductCategory> productCategoryRepo,
            IRepository<OrderDetail> orderDetailRepo,
            IRepository<Review> reviewRepo,
            IAuthHelper authHelper,
            IMediaHelper mediaHelper,
            IProductHelper productHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _categoryRepo = categoryRepo ?? throw new ArgumentNullException(nameof(categoryRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new ArgumentNullException(nameof(attributeValueRepo));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _productAttributeRepo = productAttributeRepo ?? throw new ArgumentNullException(nameof(productAttributeRepo));
            _productCategoryRepo = productCategoryRepo ?? throw new ArgumentNullException(nameof(productCategoryRepo));
            _orderDetailRepo = orderDetailRepo ?? throw new ArgumentNullException(nameof(orderDetailRepo));
            _reviewRepo = reviewRepo ?? throw new System.ArgumentNullException(nameof(reviewRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
            _mediaHelper = mediaHelper ?? throw new System.ArgumentNullException(nameof(mediaHelper));
            _productHelper = productHelper ?? throw new System.ArgumentNullException(nameof(productHelper));
        }

        public async Task<ProductsHomepage> GetHomePageProducts()
        {
            PaginatedList<Product> topPanel = await _productHelper.Get(new ProductGetRequest(0, 8, true, null, true, null, null, null, new List<string>(), 0, 0, ProductDisplay.HomepageTopPanel.Id));
            PaginatedList<Product> mostDiscounted = await _productHelper.Get(new ProductGetRequest(0, 4, true, ProductSortBy.MostDiscounted.Message, true, null, null, null, new List<string>(), 0, 0, 0));
            PaginatedList<Product> bestSelling = await _productHelper.Get(new ProductGetRequest(0, 4, true, ProductSortBy.BestSelling.Message, true, null, null, null, new List<string>(), 0, 0, 0));
            PaginatedList<Product> topRated = await _productHelper.Get(new ProductGetRequest(0, 4, true, ProductSortBy.TopRated.Message, true, null, null, null, new List<string>(), 0, 0, 0));
            PaginatedList<Product> newest = await _productHelper.Get(new ProductGetRequest(0, 8, true, ProductSortBy.Newest.Message, true, null, null, null, new List<string>(), 0, 0, 0));

            return new ProductsHomepage(
                topPanel,
                mostDiscounted,
                bestSelling,
                topRated,
                newest
            );
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

        public async Task<ReviewGetResponse> GetReviews(ReviewGetRequest input)
        {
            GetReviewByProductIdSpecification spec = new GetReviewByProductIdSpecification(input, _authHelper);
            PaginatedList<Review> items = await _reviewRepo.GetPaginated(spec);
            decimal averageRating = items.Count > 0 ? await _reviewRepo.GetAverage(spec, r => r.Rating) : 0;
            return new ReviewGetResponse(items, averageRating);
        }

        public async Task<PaginatedList<Product>> GetRecommendations(
            ProductRecommedationRequest input
        )
        {
            List<Product> list = await _orderDetailRepo.Get(new GetProductRecommendationSpecification(
                input,
                _authHelper
            ));

            list = list.GroupBy(e => e.ProductId).OrderByDescending(p => p.Count()).SelectMany(p => p).Distinct().ToList();

            List<Product> paginatedList = list.Skip(input.PageIndex * input.PageSize).Take(input.PageSize).ToList();

            return new PaginatedList<Product>(
                input.IsPagingEnabled ? paginatedList : list,
                list.Count(),
                input.PageIndex,
                input.PageSize,
                input.IsPagingEnabled
            );
        }

        public async Task<ProductResponse> GetOne(int productId)
        {
            Product product = await _productRepo.GetOne(new GetProductByIdSpecification(productId, _authHelper));
            Guard.Against.NotFound(product, nameof(Product));

            return new ProductResponse(
                product,
                await GetReviews(new ReviewGetRequest(productId)),
                await GetRecommendations(new ProductRecommedationRequest(productId))
            );
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
            _productAttributeRepo.DeleteRange(product.ProductAttribute.ToList());
            _productCategoryRepo.DeleteRange(product.ProductCategory.ToList());
            await _unitOfWork.SaveAsync();

            product.UpdateAttributes(input);
            await _unitOfWork.SaveAsync();

            return new ProductResponse(product);
        }

        public async Task<ProductResponse> Delete(int productId)
        {
            Product product = await _productRepo.GetOne(new GetProductByIdSpecification(productId, _authHelper));
            Guard.Against.NotFound(product, nameof(Product));

            await product.Remove(_mediaHelper);
            _productRepo.Delete(product);
            await _unitOfWork.SaveAsync();
            return new ProductResponse(product);
        }
    }
}