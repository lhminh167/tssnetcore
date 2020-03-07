using System.Collections.Generic;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class CreateOrderFormResponse
    {
        public CreateOrderFormResponse(List<Shipping> shippings, List<ShippingRegion> shippingRegions, List<Tax> taxes)
        {
            Guard.Against.Null(shippings, nameof(shippings));
            Guard.Against.Null(shippingRegions, nameof(shippingRegions));
            Guard.Against.Null(taxes, nameof(taxes));

            Shippings = shippings;
            ShippingRegions = shippingRegions;
            Taxes = taxes;
        }

        public List<Shipping> Shippings { get; private set; }
        public List<ShippingRegion> ShippingRegions { get; private set; }
        public List<Tax> Taxes { get; private set; }
    }
}
