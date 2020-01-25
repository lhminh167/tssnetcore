using System;
using Ardalis.GuardClauses;

namespace Tss.Core.Entities
{
    public class OrderDetail
    {
        public OrderDetail(
            Product product,
            Orders order,
            string attributes,
            int quantity
        )
        {
            Guard.Against.Null(product, nameof(product));
            Guard.Against.Null(order, nameof(order));
            Guard.Against.NullOrWhiteSpace(attributes, nameof(attributes));
            Guard.Against.BadRequest(quantity <= 0, "Invalid quantity");
            Guard.Against.BadRequest(product.Price * quantity < product.DiscountedPrice, "The total of order item is lower than applied discount");

            OrderId = order.OrderId;
            ProductId = product.ProductId;
            Attributes = attributes;
            ProductName = product.Name;
            Quantity = quantity;
            UnitCost = product.DiscountedPrice > 0 ? product.DiscountedPrice : product.Price;
        }

        public OrderDetail()
        {
        }

        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Attributes { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitCost { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
