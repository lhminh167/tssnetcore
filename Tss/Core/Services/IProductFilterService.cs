using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Models;
using System.Threading.Tasks;
using Tss.Core.Entities;

namespace Tss.Core.Services
{
    public interface IProductFilterService
    {
        Task<ProductFilterFilter> Filter();

        Task<List<Product>> Product(
            ProductFilterProductSpecifications specs
        );
    }
}
