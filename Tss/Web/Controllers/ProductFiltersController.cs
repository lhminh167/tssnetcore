using System;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.ProductFilters;
using System.Collections.Generic;

namespace Tss.Web.Controllers
{
    [Route("api/[controller]")]
    public class ProductFiltersController: ControllerBase
    {        
        private readonly IProductFiltersService _service;
        
        public ProductFiltersController(
            IProductFiltersService service
        )
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("Filters")]
        public ProductFiltersFilters Filters()
        {
            return _service.Filters();
        }

        [HttpGet("Products")]
        public object Products(
            ProductFiltersProductSpecifications specs
        )
        {
            return _service.Products(specs);
        }
    }
}