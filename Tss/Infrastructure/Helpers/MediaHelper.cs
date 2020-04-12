using System.Threading.Tasks;
using Tss.Core.Interfaces;
using Microsoft.Extensions.Options;
using Tss.Core.Models;
using Ardalis.GuardClauses;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System.Collections.Generic;
using System;
using System.Linq;

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

        public List<ImageGetUploadTokenResponse> GetImageUploadToken(ImagesGetUploadTokenRequest input)
        {
            return input.Content.Select(entry =>
            {
                double now = (
                        TimeZoneInfo.ConvertTimeToUtc(DateTime.Now)
                        - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
                    ).TotalSeconds;

                SortedDictionary<string, object> parameters = new SortedDictionary<string, object>();

                parameters.Add("public_id", entry.Name);
                parameters.Add("eager", entry.Settings);
                parameters.Add("timestamp", now);
                parameters.Add("upload_preset", "tss");

                return new ImageGetUploadTokenResponse(
                    _cloudinary.Api.SignParameters(parameters),
                    now
                );
            }).ToList();
        }

        public async Task Rename(ImagesRenameRequest input)
        {
            foreach (var entry in input.Content)
            {
                RenameResult result = await _cloudinary.RenameAsync(_settings.CloudinaryFolderName + entry.OldName, _settings.CloudinaryFolderName + entry.NewName);
            }
        }

        public async Task Delete(ImagesDeleteRequest input)
        {
            DelResParams deletionParams = new DelResParams()
            {
                PublicIds = input.Content.Select(e => _settings.CloudinaryFolderName + e.Name).ToList()
            };
            await _cloudinary.DeleteResourcesAsync(deletionParams);
        }
    }
}