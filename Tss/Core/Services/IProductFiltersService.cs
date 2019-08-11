using System.Collections.Generic;
using Tss.Core.ProductFilters;
using Tss.Core.Generics;

namespace Tss.Core.ProductFilters
{
    public interface IProductFiltersService
    {
        ProductFiltersFilters Filters();

        object Products(
            ProductFiltersProductSpecifications specs
        );
    }
}
