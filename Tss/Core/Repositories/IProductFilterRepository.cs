using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Generics;
using Tss.Core.Models;
using Tss.Core.Specifications;


namespace Tss.Core.Repositories
{
    public interface IProductFilterRepository : IRepository
    {

        Task<ProductFilterFilter> Filter();

        Task<List<Product>> Product(
            ProductFilterProductSpecifications specs
        );
    }
}
