using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Entities;
using Tss.Core.Models;
using Tss.Core.Services;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;

namespace Tss.Web.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;

        public ShoppingCartController(
            IShoppingCartService service
        )
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var dbItem = await _service.GetItems();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Shopping cart was successfully retrieved."
            };
            return Ok(response);
        }

        [HttpGet("Wishlist")]
        public async Task<IActionResult> GetSavedItems()
        {
            var dbItem = await _service.GetSavedItems();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Wishlist was successfully retrieved."
            };
            return Ok(response);
        }

        [HttpGet("TotalAmount")]
        public async Task<IActionResult> GetTotalAmount()
        {
            var dbItem = await _service.GetTotalAmount();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Total cost was successfully retrieved."
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] ShoppingCartAdd input)
        {
            var dbItem = await _service.AddItem(input);
            var status = StatusCodes.Status201Created;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Item was successfully added to cart.",
            };
            return CreatedAtAction("GetProduct", new { id = dbItem.ItemId }, response);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int itemId, [FromBody] ShoppingCartUpdate input)
        {
            var dbItem = await _service.UpdateItem(itemId, input);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Item was successfully updated."
            };
            return Ok(response);
        }

        [HttpPut("SaveItemForLater/{itemId}")]
        public async Task<IActionResult> SaveItemForLater([FromRoute] int itemId)
        {
            var dbItem = await _service.SaveItemForLater(itemId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Item was successfully saved to wishlist."
            };
            return Ok(response);
        }

        [HttpPut("MoveItemToCart/{itemId}")]
        public async Task<IActionResult> MoveItemToCart([FromRoute] int itemId)
        {
            var dbItem = await _service.MoveItemToCart(itemId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Item was successfully moved to cart."
            };
            return Ok(response);
        }
        
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> RemoveItem([FromRoute] int itemId)
        {
            var dbItem = await _service.RemoveItem(itemId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbItem,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Item was successfully removed from cart."
            };
            return Ok(response);
        }

    }
}