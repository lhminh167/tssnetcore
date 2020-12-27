using System.Threading.Tasks;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogGetResponse> Get(CatalogGetRequest input);
    }
}
