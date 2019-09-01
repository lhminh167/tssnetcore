using Tss.Core.Entities;

using Sieve.Models;
using System;
using Sieve.Services;
using System.Linq;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Services;
using Tss.Core.Repositories;
using Tss.Core.Models;

namespace Tss.Infrastructure.Services
{
    public class ProductFilterService: IProductFilterService
    {
        private readonly IProductFilterRepository _ProductFilterRepository;
        private readonly SieveProcessor _sieveProcessor;

        public ProductFilterService (
            IProductFilterRepository ProductFilterRepository,
            
            SieveProcessor sieveProcessor
        ) { 
            _ProductFilterRepository = ProductFilterRepository ?? throw new ArgumentNullException(nameof(ProductFilterRepository));
            
            _sieveProcessor = sieveProcessor ?? throw new ArgumentNullException(nameof(sieveProcessor));
        }

        public ProductFilterFilter Filter()
        {   
            
            return _ProductFilterRepository.Filter();
        }

        public List<ProductFilterProduct> Product(
            ProductFilterProductSpecifications specs
        )
        {   
    
            var sieve = new SieveModel()
            {
                // Filters = "Name@=" + specs.Keyword,
                // Page = specs.Page,
                // PageSize = specs.PageSize
            };

            var dbSet = _ProductFilterRepository.Product(specs);

            return _sieveProcessor
                .Apply<ProductFilterProduct>(sieve, dbSet)
                .ToList();
        }
    }
}