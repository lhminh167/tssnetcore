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
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Attribute values were successfully retrieved.",
                await _service.Get(name)
            );
            return Ok(response);
        }

        [HttpGet("{attributeValueId}")]
        public async Task<IActionResult> GetOne([FromRoute] int attributeValueId)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Attribute value was successfully retrieved.",
                await _service.GetOne(attributeValueId)
            );
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeValueCreateRequest input)
        {
            AttributeValue content = await _service.Create(input);
            Response response = new Response
            (            
                HttpStatusCode.Created,
                "Attribute value was created successfully.",
                content
            );
            return CreatedAtAction(nameof(GetOne), new { attributeValueId = content.AttributeValueId }, response);
        }

        [HttpPut("{attributeValueId}")]
        public async Task<IActionResult> Update([FromBody] AttributeValue input)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Attribute value was updated successfully.",
                await _service.Update(input)
            );
            return Ok(response);
        }

        [HttpDelete("{attributeValueId}")]
        public async Task<IActionResult> Delete([FromRoute] int attributeValueId)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Attribute value was successfully deleted.",
                await _service.Delete(attributeValueId)
            );
            return Ok(response);
        }
    }
}