using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Tss.Core.Models;
using Tss.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Tss.Web.Controllers
{
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(
            IReviewService service
        )
        {
            _service = service ?? throw new System.ArgumentNullException(nameof(service));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int productId,
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] bool isPagingEnabled,
            [FromQuery] string sortBy,
            [FromQuery] bool isDescending
        )
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Reviews were successfully retrieved.",
                await _service.Get(new ReviewGetRequest(
                    pageIndex,
                    pageSize,
                    isPagingEnabled,
                    sortBy,
                    isDescending,
                    productId
                ))
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetOne([FromRoute] int reviewId)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Review was successfully retrieved.",
                await _service.GetOne(reviewId)
            );
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateRequest input)
        {
            Review content = await _service.Create(input);
            Response response = new Response
            (
                HttpStatusCode.Created,
                "Review was created successfully.",
                content
            );
            return CreatedAtAction(nameof(GetOne), new { reviewId = content.ReviewId }, response);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> Update([FromBody] ReviewUpdateRequest input)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Review was updated successfully.",
                await _service.Update(input)
            );
            return Ok(response);
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> Delete([FromRoute] int reviewId)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Review was successfully deleted.",
                await _service.Delete(reviewId)
            );
            return Ok(response);
        }
    }
}