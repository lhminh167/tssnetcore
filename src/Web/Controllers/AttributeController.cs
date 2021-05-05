using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;
using Tss.Core.Models;
using Tss.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Tss.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _service;

        public AttributeController(
            IAttributeService service
        )
        {
            _service = service ?? throw new System.ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Attributes were successfully retrieved.",
                await _service.Get()
            );
            return Ok(response);
        }

        [HttpGet("{attributeId}")]
        public async Task<IActionResult> GetAttribute([FromRoute] int attributeId)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Attribute was successfully retrieved.",
                await _service.Get(attributeId)
            );
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeCreateRequest input)
        {
            Attribute content = await _service.Create(input);
            Response response = new Response
            (            
                HttpStatusCode.Created,
                "Attribute was created successfully.",
                content
            );
            return CreatedAtAction(nameof(GetAttribute), new { attributeId = content.AttributeId }, response);
        }

        [HttpPut("{attributeId}")]
        public async Task<IActionResult> Update([FromBody] AttributeUpdateRequest input)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Attribute was updated successfully.",
                await _service.Update(input)
            );
            return Ok(response);
        }

        [HttpDelete("{attributeId}")]
        public async Task<IActionResult> Delete([FromRoute] int attributeId)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Attribute was successfully deleted.",
                await _service.Delete(attributeId)
            );
            return Ok(response);
        }
    }
}