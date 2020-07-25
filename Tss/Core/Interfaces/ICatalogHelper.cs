using System.Threading.Tasks;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface ICatalogHelper
    {
        Task<CatalogGetResponse> Get(CatalogGetRequest input);
    }
}
