using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Tss.Core.Models;
using System.Net;

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
        public async Task<IActionResult> GetItems(
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] bool isPagingEnabled,
            [FromQuery] string sortBy,
            [FromQuery] bool isDescending,
            [FromQuery] bool buyNow
        )
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Shopping cart was successfully retrieved.",
                await _service.GetItems(new ShoppingCartGetRequest(
                    pageIndex,
                    pageSize,
                    isPagingEnabled,
                    sortBy,
                    isDescending,
                    buyNow
                ))
            );
            return Ok(response);
        }

        [HttpGet("Recommendation")]
        public async Task<IActionResult> GetRecommendations(
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] bool isPagingEnabled,
            [FromQuery] string sortBy,
            [FromQuery] bool isDescending
        )
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Shopping cart was successfully retrieved.",
                await _service.GetRecommendations(new PaginationRequest(
                    pageIndex,
                    pageSize,
                    isPagingEnabled,
                    sortBy,
                    isDescending
                ))
            );
            return Ok(response);
        }

        [HttpGet("TotalQuantity")]
        public async Task<IActionResult> GetTotalQuantity([FromQuery] bool buyNow)
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Total quantity was successfully retrieved.",
                await _service.GetTotalQuantity(buyNow)
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] ShoppingCartItemAddRequest input)
        {
            Response response = new Response
            (
                HttpStatusCode.Created,
                "Item was successfully added to cart.",
                await _service.AddItem(input)
            );
            return CreatedAtAction(nameof(GetItems), response);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int itemId, [FromBody] ShoppingCartItemUpdateRequest input)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Item was successfully updated.",
                await _service.UpdateItem(itemId, input)
            );
            return Ok(response);
        }

        [HttpPut("SaveItemForLater/{itemId}")]
        public async Task<IActionResult> SaveItemForLater([FromRoute] int itemId)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Item was successfully saved to wishlist.",
                await _service.SaveItemForLater(itemId)
            );
            return Ok(response);
        }

        [HttpPut("MoveItemToCart/{itemId}")]
        public async Task<IActionResult> MoveItemToCart([FromRoute] int itemId)
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Item was successfully moved to cart.",
                await _service.MoveItemToCart(itemId)
            );
            return Ok(response);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> RemoveItem([FromRoute] int itemId)
        {
            Response response = new Response(
                HttpStatusCode.OK,
                "Item was successfully removed from cart.",
                await _service.RemoveItem(itemId)
            );
            return Ok(response);
        }

    }
}