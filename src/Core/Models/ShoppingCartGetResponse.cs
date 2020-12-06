using System.Linq;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ShoppingCartGetResponse : PaginatedList<ShoppingCartResponse>
    {
        public ShoppingCartGetResponse(
            PaginatedList<ShoppingCart> list,
            double totalCost,
            int totalQuantity
        )
        : base(
            list.Items.Select(e => new ShoppingCartResponse(e)).ToList(),
            list.Count,
            list.PageIndex,
            list.PageSize,
            list.IsPagingEnabled
        )
        {
            TotalQuantity = totalQuantity;
            TotalCost = totalCost;
        }

        public int TotalQuantity { get; set; }
        public double TotalCost { get; set; }
    }
}
