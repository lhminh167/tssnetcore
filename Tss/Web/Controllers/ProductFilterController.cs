using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tss.Core.Services;
using Tss.Core.Models;
using Tss.Core.Specifications;
using Tss.Core.Constants;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    [Route("api/[controller]")]
    public class ProductFilterController: ControllerBase
    {        
        private readonly IProductFilterService _service;
        
        public ProductFilterController(
            IProductFilterService service
        )
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> Filter()
        {
            var dbItem = await _service.Filter();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = null
            };
            return Ok(response);
        }

        [HttpGet("Product")]
        public async Task<IActionResult> Product(
            ProductFilterProductSpecifications specs
        )
        {
            var dbItem = await _service.Product(specs);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Product list was successfully retrieved."
            };
            return Ok(response);
        }
    }
}