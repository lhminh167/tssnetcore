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
        public async Task<IActionResult> Get(
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] bool isPagingEnabled,
            [FromQuery] string sortBy,
            [FromQuery] bool isDescending
        )
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Orders were successfully retrieved.",
                await _service.Get(new PaginationRequest(
                    pageIndex,
                    pageSize,
                    isPagingEnabled,
                    sortBy,
                    isDescending
                ))
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
        public async Task<IActionResult> GetCreateForm()
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Checkout form was successfully retrieved.",
                await _service.GetCreateForm()
            );
            return Ok(response);
        }

        [HttpPost("Payment")]
        public async Task<IActionResult> CreatePayment([FromBody] OrderCreateRequest input)
        {
            OrderCreateResponse content = await _service.CreatePayment(input);
            Response response = new Response
            (
                HttpStatusCode.Created,
                "Payment was created successfully.",
                content
            );
            return CreatedAtAction(nameof(GetOne), new { orderId = content.Order.OrderId }, response);
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
                "Thank you for confirming the stock. A notification email will be sent to the customer.",
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
                "Thank you for confirming the stock. The order was cancelled. A notification email will be sent to the customer.",
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
                "Thank you for confirming that the order has been delivered. A notification email will be sent to the customer.",
                await _service.ShipOk(orderId)
            );
            return Ok(response);
        }
    }
}