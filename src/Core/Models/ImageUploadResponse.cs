
using System;
using System.Net;

namespace Tss.Core.Models
{

    public class ImageUploadResponse : Response
    {
        public ImageUploadResponse(
            Uri uri,
            string version,
            HttpStatusCode status,
            string title,
            string detail
        )
        : base(
            status,
            title,
            detail
        )
        {
        }

        public static ImageUploadResponse NoResponse = new ImageUploadResponse(
            null,
            "",
            HttpStatusCode.ServiceUnavailable,
            "Unable to upload image now. Please try again later.",
            "This error indicates that the server is temporarily unavailable, usually due to high load or maintenance."
        );

        public string Version { get; private set; }
        public Uri Uri { get; private set; }
    }
}