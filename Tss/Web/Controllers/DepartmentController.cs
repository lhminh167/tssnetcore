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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(
            IDepartmentService service
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
                "Departments were successfully retrieved.",
                await _service.Get()
            );
            return Ok(response);
        }

        // [HttpGet("{DepartmentId}")]
        // public async Task<IActionResult> GetDepartment([FromRoute] int DepartmentId)
        // {
        //     Response response = new Response
        //     (            
        //         HttpStatusCode.OK,
        //         "Department was successfully retrieved.",
        //         await _service.Get(DepartmentId)
        //     );
        //     return Ok(response);
        // }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] DepartmentCreateRequest input)
        // {
        //     Department content = await _service.Create(input);
        //     Response response = new Response
        //     (            
        //         HttpStatusCode.Created,
        //         "Department was created successfully.",
        //         content
        //     );
        //     return CreatedAtAction(nameof(GetDepartment), new { DepartmentId = content.DepartmentId }, response);
        // }

        // [HttpPut("{DepartmentId}")]
        // public async Task<IActionResult> Update([FromBody] Department input)
        // {
        //     Response response = new Response
        //     (            
        //         HttpStatusCode.OK,
        //         "Department was updated successfully.",
        //         await _service.Update(input)
        //     );
        //     return Ok(response);
        // }

        // [HttpDelete("{DepartmentId}")]
        // public async Task<IActionResult> Delete([FromRoute] int DepartmentId)
        // {
        //     Response response = new Response
        //     (            
        //         HttpStatusCode.OK,
        //         "Department was successfully deleted.",
        //         await _service.Delete(DepartmentId)
        //     );
        //     return Ok(response);
        // }
    }
}