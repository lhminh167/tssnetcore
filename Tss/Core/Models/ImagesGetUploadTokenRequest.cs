using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ImagesGetUploadTokenRequest
    {
        // public ImagesGetUploadTokenRequest(List<ImageGetUploadTokenRequest> input)
        // {
        //     Guard.Against.Null(input, nameof(input));

        //     Content = input;
        // }

        public List<ImageGetUploadTokenRequest> Content { get; set; }
    }
}
