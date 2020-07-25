using System;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tss.Core.Models;
using System.Net;

namespace Tss.Web.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogHelper _helper;

        public CatalogController(
            ICatalogHelper helper
        )
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] bool shipping,
            [FromQuery] bool shippingRegion,
            [FromQuery] bool tax,
            [FromQuery] bool role
        )
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Catalog were successfully retrieved.",
                await _helper.Get(new CatalogGetRequest(
                    shipping,
                    shippingRegion,
                    tax,
                    role
                ))
            );
            return Ok(response);
        }
    }
}