using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;

namespace Tss.Core.Entities
{
    public partial class Orders : IAggregateRoot
    {

        public Orders(
            List<ShoppingCart> shoppingCart,
            Customer customer,
            Shipping shipping,
            Tax tax,
            ShippingRegion shippingRegion
        )
        {
            Guard.Against.Null(shoppingCart, nameof(ShoppingCart));
            Guard.Against.OutOfRange(shoppingCart.Count, nameof(shoppingCart.Count), 1, int.MaxValue);
            Guard.Against.Null(customer, nameof(Customer));
            Guard.Against.Null(shipping, nameof(Shipping));
            Guard.Against.Null(tax, nameof(Tax));
            Guard.Against.Null(shippingRegion, nameof(ShippingRegion));
            Guard.Against.BadRequest(
                shipping.ShippingRegionId == shippingRegion.ShippingRegionId,
                "This shipping option is not available at your chosen region."
            );

            CreatedOn = DateTime.Now;
            Status = OrderStatus.OrderPlacedNotifyingCustomer.Id;
            ShippingId = shipping.ShippingId;
            CustomerId = customer.CustomerId;
            TaxId = tax.TaxId;

            Tax = tax;
            Customer = customer;
            Shipping = shipping;
            Audit = new List<Audit>();
            OrderDetail = shoppingCart.Select(item => new OrderDetail(
                item.Product,
                this,
                item.Attributes,
                item.Quantity
            )).ToList();
            SetTotalAmount();
        }

        public Orders()
        {
            Audit = new HashSet<Audit>();
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ShippedOn { get; set; }
        public int Status { get; set; }
        public string Comments { get; set; }
        public int CustomerId { get; set; }
        public string AuthCode { get; set; }
        public string Reference { get; set; }
        public int ShippingId { get; set; }
        public int TaxId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual Tax Tax { get; set; }
        public virtual ICollection<Audit> Audit { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual OrderStatus OrderStatus => OrderStatus.FromValue<OrderStatus>(Status);

        private void SetOrderStatus(OrderStatus status)
        {
            Guard.Against.Null(status, nameof(status));

            Status = status.Id;
        }

        private void SetDateShipped()
        {
            ShippedOn = DateTime.Now;
        }

        private void CreateAudit(OrderAudit entry)
        {
            Guard.Against.Null(entry, nameof(entry));

            Audit audit = new Audit(
                entry.Message,
                entry.Id,
                OrderId
            );

            Audit.Add(audit);
        }

        private double GetTaxAmount()
        {
            return TotalAmount * Tax.TaxPercentage / 100.00;
        }

        private void SetTotalAmount()
        {
            TotalAmount = OrderDetail.Sum(o => o.Quantity * o.UnitCost);
            TotalAmount += Shipping.ShippingCost;
            TotalAmount += GetTaxAmount();
        }
    }
}
