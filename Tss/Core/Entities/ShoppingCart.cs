using System;
using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart(ShoppingCartItemAddRequest input, string cartId)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.NullOrWhiteSpace(cartId, nameof(cartId));
            Guard.Against.OutOfRange(input.Quantity, nameof(Quantity), 0, int.MaxValue);

            ProductId = input.ProductId;
            CartId = cartId;
            Attributes = input.Attributes;
            Quantity = input.Quantity;
            BuyNow = input.BuyNow;
            AddedOn = DateTime.Now;
        }

        private ShoppingCart()
        {
        }

        public int ItemId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public string Attributes { get; set; }
        public int Quantity { get; set; }
        public bool BuyNow { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual Product Product { get; set; }

        public void UpdateItem(ShoppingCartItemUpdateRequest input)
        {
            Quantity = input.Quantity;
            AddedOn = DateTime.Now;
        }

        public void SaveItemForLater()
        {
            AddedOn = DateTime.Now;
            BuyNow = false;
        }

        public void AddUpQuantity(int quantity)
        {
            Quantity += quantity;
            AddedOn = DateTime.Now;
        }

        public void MoveItemToCart()
        {
            AddedOn = DateTime.Now;
            BuyNow = true;
        }

        public void AssociateToCustomer(int customerId)
        {
            CartId = customerId.ToString();
        }
    }
}
