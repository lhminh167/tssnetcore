using System.Threading.Tasks;
using Tss.Core.Entities;

namespace Tss.Core.Interfaces
{
    public interface IMediaHelper
    {
        Task UploadImage(IEmail email);
    }
}