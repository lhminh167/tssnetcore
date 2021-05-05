using System.Collections.Generic;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ShoppingCartItemAddResponse
    {
        public ShoppingCartItemAddResponse() { }

        public ShoppingCartResponse Item { get; set; }
        public int TotalQuantity { get; set; }
        public string LoginToken { get; set; }
    }
}
