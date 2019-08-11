using Tss.Core.Entities;
using Tss.Core.Generics;
using Sieve.Models;
using System;
using Sieve.Services;
using System.Linq;
using Tss.Core.ProductFilters;

namespace Tss.Infrastructure.Services
{
    public class ProductFiltersService: IProductFiltersService
    {
        private readonly IProductFiltersRepository _ProductFiltersRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<AttributeValue> _attributeValueRepository;
        private readonly SieveProcessor _sieveProcessor;

        public ProductFiltersService (
            IProductFiltersRepository ProductFiltersRepository,
            IRepository<Category> categoryRepository,
            IRepository<AttributeValue> attributeValueRepository,
            SieveProcessor sieveProcessor
        ) { 
            _ProductFiltersRepository = ProductFiltersRepository ?? throw new ArgumentNullException(nameof(ProductFiltersRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _attributeValueRepository = attributeValueRepository ?? throw new ArgumentNullException(nameof(attributeValueRepository));
            _sieveProcessor = sieveProcessor ?? throw new ArgumentNullException(nameof(sieveProcessor));
        }

        public ProductFiltersFilters Filters()
        {   
            var colorsSieve = new SieveModel()
            {
                Filters = "attributeId==2",
            };

            var sizesSieve = new SieveModel
            {
                Filters = "attributeId==1",
            };

            var categoriesSieve = new SieveModel
            {
            };

            var colorsDbSet = _attributeValueRepository.Get();
            var sizesDbSet = _attributeValueRepository.Get();
            var categoriesDbSet = _categoryRepository.Get();

            ProductFiltersFilters productFilters = new ProductFiltersFilters {
                Colors = _sieveProcessor.Apply<AttributeValue>(colorsSieve, colorsDbSet).ToList(),
                Sizes = _sieveProcessor.Apply<AttributeValue>(sizesSieve, sizesDbSet).ToList(),
                Categories = _sieveProcessor.Apply<Category>(categoriesSieve, categoriesDbSet).ToList()
            };

            return productFilters;
        }

        public object Products(
            ProductFiltersProductSpecifications specs
        )
        {   
    
            var sieve = new SieveModel()
            {
                // Filters = "Name@=" + specs.Keyword,
                // Page = specs.Page,
                // PageSize = specs.PageSize
            };

            var dbSet = _ProductFiltersRepository.Products(specs);

            return _sieveProcessor
                .Apply<object>(sieve, dbSet)
                .ToList();;
        }
    }
}