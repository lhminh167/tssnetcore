using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ShippingRegionResponse
    {
        public ShippingRegionResponse(ShippingRegion input)
        {
            if (input is null) return;

            ShippingRegionId = input.ShippingRegionId;
            ShippingRegion1 = input.ShippingRegion1;

            Shipping = new List<ShippingResponse>();
        }

        public ShippingRegionResponse()
        {
            Shipping = new List<ShippingResponse>();
        }

        public int ShippingRegionId { get; set; }
        public string ShippingRegion1 { get; set; }

        public virtual List<ShippingResponse> Shipping { get; set; }
    }
}
