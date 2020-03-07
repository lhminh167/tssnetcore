using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Tss.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Tss.Core.Entities;

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
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Orders were successfully retrieved.",
                await _service.Get()
            );
            return Ok(response);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOne([FromRoute] int orderId)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Order was successfully retrieved.",
                await _service.Get(orderId)
            );
            return Ok(response);
        }

        [HttpGet("New")]
        public async Task<IActionResult> GetGetCreateOrderForm()
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Checkout form was successfully retrieved.",
                await _service.GetCreateOrderForm()
            );
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateRequest input)
        {
            OrderCreateResponse content = await _service.Create(input);
            Response response = new Response
            (
                HttpStatusCode.Created,
                "Your order was placed successfully.",
                content
            );
            return CreatedAtAction(nameof(GetOne), new { orderId = content.Order.OrderId }, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("StockOk/{orderId}")]
        public async Task<IActionResult> StockOk([FromRoute] int orderId)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Thank you for confirming the stock. We will send you shipping notiication once payment was taken.",
                await _service.StockOk(orderId)
            );
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("StockNotOk/{orderId}")]
        public async Task<IActionResult> StockNotOk([FromRoute] int orderId, [FromBody] string comments)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Thank you for confirming the stock. We will send you shipping notiication once payment was taken.",
                await _service.StockNotOk(orderId, comments)
            );
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("ShipOk/{orderId}")]
        public async Task<IActionResult> ShipOk([FromRoute] int orderId)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Thank you for confirming that the order has been dispatched. We will now send a notification to customer.",
                await _service.ShipOk(orderId)
            );
            return Ok(response);
        }
    }
}