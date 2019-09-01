using System.Linq;
using Tss.Core.Generics;
using Tss.Core.Models;
using Tss.Core.Specifications;


namespace Tss.Core.Repositories
{
    public interface IProductFilterRepository : IRepository
    {

        ProductFilterFilter Filter();

        IQueryable<ProductFilterProduct> Product(
            ProductFilterProductSpecifications specs
        );
    }
}
