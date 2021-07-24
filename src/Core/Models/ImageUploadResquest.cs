using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Tss.Core.Models
{
    public class ImageUploadResquest
    {
        public ImageUploadResquest(
            string name,
            string settings
        )
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NullOrWhiteSpace(settings, nameof(settings));

            Name = name;
            Settings = settings;
        }

        public string Settings { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
