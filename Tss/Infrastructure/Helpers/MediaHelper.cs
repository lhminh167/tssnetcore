using System.Threading.Tasks;
using Tss.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using Tss.Core.Models;
using Ardalis.GuardClauses;
using Tss.Core.Entities;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace Microsoft.eShopWeb.Infrastructure.Helpers
{
    public class MediaHelper : IMediaHelper
    {
        private readonly AppSettings _settings;
        private readonly Account _account;
        private readonly Cloudinary _cloudinary;
    
        public MediaHelper(
            IOptions<AppSettings> settings
        )
        {
            Guard.Against.Null(settings, nameof(settings));
            Guard.Against.Null(settings.Value, nameof(settings.Value));

            _settings = settings.Value;

            _account = new Account(
                _settings.CloudinaryCloudName,
                _settings.CloudinaryApiKey,
                _settings.CloudinaryApiSecret
            );

            _cloudinary = new Cloudinary(_account);
        }

        public async Task UploadImage(IEmail email)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"http://www.example.com/mysample.jpg"),
                PublicId = "sample_woman"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
// _cloudinary.Api.SignParameters(IDictionary<string, object> parameters)

            return;
        }
    }
}