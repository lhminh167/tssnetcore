using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;
using Tss.Core.Models;
using Tss.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Collections.Generic;

namespace Tss.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaHelper _mediaHelper;

        public MediaController(
            IMediaHelper mediaHelper
        )
        {
            _mediaHelper = mediaHelper ?? throw new System.ArgumentNullException(nameof(mediaHelper));
        }

        [HttpPost("ImageUploadToken")]
        public IActionResult GetImageUploadToken([FromBody] ImagesGetUploadTokenRequest input)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Medias were successfully retrieved.",
                _mediaHelper.GetImageUploadToken(input)
            );
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Rename([FromBody] ImagesRenameRequest input)
        {
            await _mediaHelper.Rename(input);
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Medias were successfully retrieved.",
                null
            );
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ImagesDeleteRequest input)
        {
            await _mediaHelper.Delete(input);
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Medias were successfully retrieved.",
                null
            );
            return Ok(response);
        }
    }
}