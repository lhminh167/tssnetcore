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
    public class AttributeValueController : ControllerBase
    {
        private readonly IAttributeValueService _service;

        public AttributeValueController(
            IAttributeValueService service
        )
        {
            _service = service ?? throw new System.ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name)
        {
            var dbAttributeValue = await _service.Get(name);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbAttributeValue,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attribute values were successfully retrieved."
            };
            return Ok(response);
        }

        [HttpGet("{attributeValueId}")]
        public async Task<IActionResult> GetOne([FromRoute] int attributeValueId)
        {
            var dbAttributeValue = await _service.GetOne(attributeValueId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbAttributeValue,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attribute value was successfully retrieved."
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeValueCreateRequest input)
        {
            var dbAttributeValue = await _service.Create(input);
            var status = StatusCodes.Status201Created;
            var response = new Response
            {
                Content = null,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attribute value was created successfully.",
            };
            return CreatedAtAction(nameof(GetOne), new { attributeValueId = dbAttributeValue.AttributeValueId }, response);
        }

        [HttpPut("{attributeValueId}")]
        public async Task<IActionResult> Update([FromBody] AttributeValue input)
        {
            var dbAttributeValue = await _service.Update(input);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbAttributeValue,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attribute value was updated successfully.",
            };
            return Ok(response);
        }

        [HttpDelete("{attributeValueId}")]
        public async Task<IActionResult> Delete([FromRoute] int attributeValueId)
        {
            var dbAttributeValue = await _service.Delete(attributeValueId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbAttributeValue,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Attribute value was successfully deleted."
            };
            return Ok(response);
        }
    }
}