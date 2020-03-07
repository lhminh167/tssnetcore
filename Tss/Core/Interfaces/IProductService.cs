using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductFilterResponse> GetFilters();
        Task<PaginatedList<Product>> Get(ProductSearchRequest input);
        Task<ProductResponse> GetOne(int productId);
        Task<ProductResponse> Create(ProductCreateRequest input);
        Task<ProductResponse> Update(ProductUpdateRequest input);
        Task<ProductResponse> Delete(int productId);
    }
}
