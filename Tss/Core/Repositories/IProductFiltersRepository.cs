using System.Linq;
using Tss.Core.ProductFilters;
using Tss.Core.Generics;

namespace Tss.Core.ProductFilters
{
    public interface IProductFiltersRepository : IRepository
    {
        IQueryable<ProductFiltersProduct> Products(
            ProductFiltersProductSpecifications specs
        );
    }
}
