using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tss.Core.Services;
using Tss.Core.Models;
using Tss.Core.Specifications;

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
        public ProductFilterFilter Filter()
        {
            return _service.Filter();
        }

        [HttpGet("Product")]
        public List<ProductFilterProduct> Product(
            ProductFilterProductSpecifications specs
        )
        {
            return _service.Product(specs);
        }
    }
}