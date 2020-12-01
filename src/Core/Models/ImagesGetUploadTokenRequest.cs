using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ImagesGetUploadTokenRequest
    {
        public List<ImageGetUploadTokenRequest> Content { get; set; }
    }
}
