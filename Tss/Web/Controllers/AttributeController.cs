using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;
using Tss.Core.Models;
using Tss.Core.Entities;
using Microsoft.AspNetCore.Authorization;

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
            var dbAttribute = await _service.Get();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbAttribute,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attributes were successfully retrieved."
            };
            return Ok(response);
        }

        [HttpGet("{attributeId}")]
        public async Task<IActionResult> GetAttribute([FromRoute] int attributeId)
        {
            var dbAttribute = await _service.Get(attributeId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbAttribute,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attributes were successfully retrieved."
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeCreateRequest input)
        {
            var dbAttribute = await _service.Create(input);
            var status = StatusCodes.Status201Created;
            var response = new Response
            {
                Content = null,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attribute was created successfully.",
            };
            return CreatedAtAction(nameof(GetAttribute), new { attributeId = dbAttribute.AttributeId }, response);
        }

        [HttpPut("{attributeId}")]
        public async Task<IActionResult> Update([FromBody] Attribute input)
        {
            var dbAttribute = await _service.Update(input);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbAttribute,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attribute was updated successfully.",
            };
            return Ok(response);
        }

        [HttpDelete("{attributeId}")]
        public async Task<IActionResult> Delete([FromRoute] int attributeId)
        {
            var dbAttribute = await _service.Delete(attributeId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbAttribute,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attribute was successfully deleted."
            };
            return Ok(response);
        }
    }
}