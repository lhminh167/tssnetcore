using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductFilterResponse> GetFilters();
        Task<List<Product>> Get();
        Task<Product> GetOne(int productId);
        Task<Product> Create(ProductCreateRequest input);
        Task<Product> Update(Product input);
        Task<Product> Delete(int productId);
    }
}
