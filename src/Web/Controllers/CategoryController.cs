using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Tss.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Tss.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(
            ICategoryService service
        )
        {
            _service = service ?? throw new System.ArgumentNullException(nameof(service));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Categories were successfully retrieved.",
                await _service.Get()
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetCategory([FromRoute] int CategoryId)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Category was successfully retrieved.",
                await _service.Get(CategoryId)
            );
            return Ok(response);
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] CategoryCreateRequest input)
        // {
        //     Category content = await _service.Create(input);
        //     Response response = new Response
        //     (            
        //         HttpStatusCode.Created,
        //         "Category was created successfully.",
        //         content
        //     );
        //     return CreatedAtAction(nameof(GetCategory), new { CategoryId = content.CategoryId }, response);
        // }

        // [HttpPut("{CategoryId}")]
        // public async Task<IActionResult> Update([FromBody] Category input)
        // {
        //     Response response = new Response
        //     (            
        //         HttpStatusCode.OK,
        //         "Category was updated successfully.",
        //         await _service.Update(input)
        //     );
        //     return Ok(response);
        // }

        // [HttpDelete("{CategoryId}")]
        // public async Task<IActionResult> Delete([FromRoute] int CategoryId)
        // {
        //     Response response = new Response
        //     (            
        //         HttpStatusCode.OK,
        //         "Category was successfully deleted.",
        //         await _service.Delete(CategoryId)
        //     );
        //     return Ok(response);
        // }
    }
}