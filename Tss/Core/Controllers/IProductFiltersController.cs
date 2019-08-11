using System.Collections.Generic;
using System.Linq;
using Sieve.Models;
using Tss.Core.ProductFilters;
using Tss.Core.Generics;

namespace Tss.Core.ProductFilters
{
    public interface IProductFiltersController
    {
        ProductFiltersFilters Filters();
        
        object Products(
            ProductFiltersProductSpecifications specs
        );
    }
}
