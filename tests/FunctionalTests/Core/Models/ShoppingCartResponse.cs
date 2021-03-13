using System;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ShoppingCartResponse
    {
        public ShoppingCartResponse()
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
