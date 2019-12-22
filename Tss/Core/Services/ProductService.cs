using Tss.Core.Entities;
using System;
using Sieve.Services;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;

namespace Tss.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<AttributeValue> _attributeValueRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IAuthHelper _authHelper;

        public ProductService(
            IRepository<Category> categoryRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IRepository<Product> productRepo,
            IAuthHelper authHelper
        )
        {
            _categoryRepo = categoryRepo ?? throw new ArgumentNullException(nameof(categoryRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new ArgumentNullException(nameof(attributeValueRepo));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        public async Task<ProductFilter> GetFilters()
        {
            List<string> attributes = new List<string>{"Color", "Size"};
            List<AttributeValue> attributeValues = await _attributeValueRepo.Get(new GetAttributeValueByNamesAndValuesSpecification(attributes, new List<string>(), _authHelper));
            List<ProductFilterCategoryResponse> categories = await _categoryRepo.Get(new GetCategorySelectListSpecification());
            return new ProductFilter(attributeValues, categories);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productRepo.Get(new GetProductSpecification(_authHelper));
        }
    }
}