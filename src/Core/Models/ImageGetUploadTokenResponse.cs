using System;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ImageGetUploadTokenResponse
    {
        public ImageGetUploadTokenResponse(
            string uploadToken,
            double creationTime
        )
        {
            Guard.Against.NullOrWhiteSpace(uploadToken, nameof(uploadToken));
            Guard.Against.NullOrWhiteSpace(creationTime.ToString(), nameof(creationTime));

            UploadToken = uploadToken;
            CreationTime = creationTime;
        }

        public string UploadToken { get; set; }
        public double CreationTime { get; set; }
    }
}
