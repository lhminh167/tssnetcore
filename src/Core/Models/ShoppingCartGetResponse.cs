using System.Linq;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ShoppingCartGetResponse : PaginatedList<ShoppingCart>
    {
        public ShoppingCartGetResponse(
            PaginatedList<ShoppingCart> list,
            double totalCost,
            int totalQuantity
        )
        : base(
            list.Items.ToList(),
            list.Count,
            list.PageIndex,
            list.PageSize,
            list.IsPagingEnabled
        )
        {
            TotalQuantity = totalQuantity;
            TotalCost = totalCost;
        }

        public int TotalQuantity { get; private set; }
        public double TotalCost { get; private set; }
    }
}
