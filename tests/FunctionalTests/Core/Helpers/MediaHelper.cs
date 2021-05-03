using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class MediaHelper
    {
        private readonly Account _account;
        private readonly Cloudinary _cloudinary;

        public MediaHelper(
            WebFixture fixture
        )
        {
            _account = new Account(
                fixture.AppSettings.CloudinaryCloudName,
                fixture.AppSettings.CloudinaryApiKey,
                fixture.AppSettings.CloudinaryApiSecret
            );

            _cloudinary = new Cloudinary(_account);
        }

        public async Task Upload()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "tests", "FunctionalTests", "Web", "Fixtures", "test-image.jpg");

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path)
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
    }
}
