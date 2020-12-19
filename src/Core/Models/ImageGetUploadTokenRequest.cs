using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ImageGetUploadTokenRequest
    {
        public ImageGetUploadTokenRequest(string name, string settings)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NullOrWhiteSpace(settings, nameof(settings));

            Name = name;
            Settings = settings;
        }

        public string Settings { get; set; }
        public string Name { get; set; }
    }
}
