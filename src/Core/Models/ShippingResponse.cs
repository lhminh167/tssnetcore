using System.Collections.Generic;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ShippingResponse
    {
        public ShippingResponse(Shipping input)
        {
            if (input is null) return;

            ShippingId = input.ShippingId;
            ShippingType = input.ShippingType;
            ShippingCost = input.ShippingCost;
            ShippingRegionId = input.ShippingRegionId;

            ShippingRegion = new ShippingRegionResponse(input.ShippingRegion);
        }

        public ShippingResponse()
        {
            Orders = new HashSet<OrderResponse>();
        }

        public int ShippingId { get; set; }
        public string ShippingType { get; set; }
        public double ShippingCost { get; set; }
        public int ShippingRegionId { get; set; }

        public virtual ShippingRegionResponse ShippingRegion { get; set; }
        public virtual ICollection<OrderResponse> Orders { get; set; }
    }
}
