using System.Linq;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ShoppingCartGetResponse : PaginatedList<ShoppingCartResponse>
    {
        public ShoppingCartGetResponse() { }

        public int TotalQuantity { get; set; }
        public double TotalCost { get; set; }
    }
}
