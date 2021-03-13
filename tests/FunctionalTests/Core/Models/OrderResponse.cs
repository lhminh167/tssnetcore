using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;
using Tss.Core.Interfaces;

namespace Tss.FunctionalTests.Core.Models
{
    public class OrderResponse
    {
        public OrderResponse()
        {
            // Audit = new List<AuditResponse>();
            // OrderDetail = new List<OrderDetailResponse>();
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

        public virtual CustomerResponse Customer { get; set; }
        public virtual ShippingResponse Shipping { get; set; }
        public virtual TaxResponse Tax { get; set; }
        public virtual List<AuditResponse> Audit { get; set; }
        public virtual List<OrderDetailResponse> OrderDetail { get; set; }

        public virtual OrderStatusResponse OrderStatus { get; set; }
    }
}
