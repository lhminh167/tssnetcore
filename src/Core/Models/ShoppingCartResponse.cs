using System;
using Ardalis.GuardClauses;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Models
{
    public class ShoppingCartResponse
    {
        public ShoppingCartResponse(ShoppingCart input)
        {
            if (input is null) return;

            ItemId = input.ItemId;
            CartId = input.CartId;
            ProductId = input.ProductId;
            Attributes = input.Attributes;
            Quantity = input.Quantity;
            BuyNow = input.BuyNow;
            AddedOn = input.AddedOn;

            Product = new ProductResponse(input.Product);
        }

        private ShoppingCartResponse()
        {
        }

        public int ItemId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public string Attributes { get; set; }
        public int Quantity { get; set; }
        public bool BuyNow { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ProductResponse Product { get; set; }
    }
}
