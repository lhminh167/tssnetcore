using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;
using Tss.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace Tss.Web.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(
            IOrderService service
        )
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dbOrder = await _service.Get();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbOrder,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Orders were successfully retrieved."
            };
            return Ok(response);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get([FromRoute] int orderId)
        {
            var dbOrder = await _service.Get(orderId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbOrder,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Order was successfully retrieved."
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeliveryForm input)
        {
            var dbOrder = await _service.Create(input);
            var status = StatusCodes.Status201Created;
            var response = new Response
            {
                Content = dbOrder,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Your order was placed successfully.",
            };
            return CreatedAtAction("GetProduct", new { id = dbOrder.OrderId }, response);
        }

        [Authorize(Roles = "Supplier")]
        [HttpPut("StockOk/{orderId}")]
        public async Task<IActionResult> StockOk([FromRoute] int orderId)
        {
            var dbOrder = await _service.StockOk(orderId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbOrder,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Thank you for confirming the stock. We will send you shipping notiication once payment was taken."
            };
            return Ok(response);
        }

        [Authorize(Roles = "Supplier")]
        [HttpPut("ShipOk/{orderId}")]
        public async Task<IActionResult> ShipOk([FromRoute] int orderId)
        {
            var dbOrder = await _service.ShipOk(orderId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbOrder,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Thank you for shipping the goods. We will now send a notification to the customer."
            };
            return Ok(response);
        }
    }
}