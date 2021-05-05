using System.Collections.Generic;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Entities;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
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
            Orders = new List<OrderResponse>();
        }

        public int ShippingId { get; set; }
        public string ShippingType { get; set; }
        public double ShippingCost { get; set; }
        public int ShippingRegionId { get; set; }

        public virtual ShippingRegionResponse ShippingRegion { get; set; }
        public virtual List<OrderResponse> Orders { get; set; }
    }
}
