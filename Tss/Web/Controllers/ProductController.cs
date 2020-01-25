using System;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tss.Core.Models;
using System.Net;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {        
        private readonly IProductService _service;
        
        public ProductController(
            IProductService service
        )
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [AllowAnonymous]
        [HttpGet("Filter")]
        public async Task<IActionResult> GetFilters()
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Filters were successfully retrieved.",
                await _service.GetFilters()
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Products were successfully retrieved.",
                await _service.Get()
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetOne([FromRoute] int productId)
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Products were successfully retrieved.",
                await _service.GetOne(productId)
            );
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest input)
        {
            Product content = await _service.Create(input);
            Response response = new Response
            (
                HttpStatusCode.Created,
                "Product was created successfully.",
                content
            );
            return CreatedAtAction(nameof(GetOne), new { id = content.ProductId }, response);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update([FromBody] Product input)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Product was updated successfully.",
                await _service.Update(input)
            );
            return Ok(response);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Product was successfully deleted.",
                await _service.Delete(productId)
            );
            return Ok(response);
        }
    }
}