using System;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class OrderDetailResponse
    {
        public OrderDetailResponse()
        {
        }

        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Attributes { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitCost { get; set; }

        public virtual OrderResponse Order { get; set; }
        public virtual ProductResponse Product { get; set; }
    }
}
