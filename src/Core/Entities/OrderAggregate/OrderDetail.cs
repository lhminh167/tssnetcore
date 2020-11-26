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

        public int ItemId { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public string Attributes { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public double UnitCost { get; private set; }

        public virtual Orders Order { get; private set; }
        public virtual Product Product { get; private set; }
    }
}
