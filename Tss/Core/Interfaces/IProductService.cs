using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductFilter> GetFilters();
        Task<List<Product>> GetProducts();
    }
}
