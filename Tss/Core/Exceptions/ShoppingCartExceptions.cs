using System;
using Tss.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Tss.Core.Exceptions
{
    public class ShoppingCartProductNotFoundException : Exception
    {
        public ShoppingCartProductNotFoundException(ShoppingCartAdd input)
        : base(
            "Product with such options is unavailable. Please choose another option or product."
        )
        {
            var status = StatusCodes.Status404NotFound;
            var response = new Response
            {
                Status = status,
                Title = base.Message,
                Detail = $"Product with id {input.ProductId}, attributes {input.Attributes} was not found in the database."
            };

            base.Data.Add("Response", response);
        }
    }

    public class ShoppingCartItemNotFoundException : Exception
    {

        public ShoppingCartItemNotFoundException(int itemId)
        : base(
            "Item not found. To update or remove an item, please add it to cart first."
        )
        {
            var status = StatusCodes.Status404NotFound;
            var response = new Response
            {
                Status = status,
                Title = base.Message,
                Detail = $"Item with id {itemId} does not exist in the database."
            };

            base.Data.Add("Response", response);
        }
    }


    public class ShoppingCartUnauthorizedActionException : Exception
    {

        public ShoppingCartUnauthorizedActionException()
        : base(
            "You are not authorized to access this item. Please log in or choose another item."
        )
        {
            var status = StatusCodes.Status401Unauthorized;
            var response = new Response
            {
                Status = status,
                Title = base.Message,
                Detail = $"The cartId of item found in database and of user are not the same. This means the user does not own this shopping cart."
            };

            base.Data.Add("Response", response);
        }
    }
}
