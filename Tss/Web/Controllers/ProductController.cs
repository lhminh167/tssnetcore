using System;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Tss.Core.Constants;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tss.Core.Models;

namespace Tss.Web.Controllers
{
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
            var dbItem = await _service.GetFilters();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Filters were successfully retrieved."
            };
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var dbItem = await _service.GetProducts();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Products were successfully retrieved."
            };
            return Ok(response);
        }
    }
}