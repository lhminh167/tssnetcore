using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IProductHelper
    {
        Task<PaginatedList<Product>> Get(ProductSearchRequest input);
    }
}
