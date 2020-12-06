using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ShippingRegionResponse
    {
        public ShippingRegionResponse(ShippingRegion input)
        {
            if (input is null) return;

            ShippingRegionId = input.ShippingRegionId;
            ShippingRegion1 = input.ShippingRegion1;

            Shipping = new HashSet<ShippingResponse>();
        }

        public ShippingRegionResponse()
        {
            Shipping = new HashSet<ShippingResponse>();
        }

        public int ShippingRegionId { get; set; }
        public string ShippingRegion1 { get; set; }

        public virtual ICollection<ShippingResponse> Shipping { get; set; }
    }
}
