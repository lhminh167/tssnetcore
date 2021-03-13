using System;
using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ShoppingCart
    {
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

        public void Update(ShoppingCartItemUpdateRequest input)
        {
            Quantity = input.Quantity;
            AddedOn = DateTime.Now;
        }

        public void AddUpQuantity(int quantity)
        {
            Quantity += quantity;
            AddedOn = DateTime.Now;
        }

        public void ChangeBuyNow()
        {
            AddedOn = DateTime.Now;
            BuyNow = !BuyNow;
        }

        public void AssociateToCustomer(int customerId)
        {
            CartId = customerId.ToString();
        }
    }
}
