using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IMediaHelper
    {
        List<ImageGetUploadTokenResponse> GetImageUploadToken(ImagesGetUploadTokenRequest input);
        Task Rename(ImagesRenameRequest input);
        Task Delete(ImagesDeleteRequest input);
    }
}