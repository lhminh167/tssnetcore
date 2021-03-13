using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class OrderCreateFormResponse
    {
        public OrderCreateFormResponse(List<Shipping> shippings, List<ShippingRegion> shippingRegions, List<Tax> taxes)
        {
            Guard.Against.Null(shippings, nameof(shippings));
            Guard.Against.Null(shippingRegions, nameof(shippingRegions));
            Guard.Against.Null(taxes, nameof(taxes));

            Shippings = shippings.Select(e => new ShippingResponse(e)).ToList();
            ShippingRegions = shippingRegions.Select(e => new ShippingRegionResponse(e)).ToList();
            Taxes = taxes.Select(e => new TaxResponse(e)).ToList();
        }

        public OrderCreateFormResponse() { }

        public List<ShippingResponse> Shippings { get; set; }
        public List<ShippingRegionResponse> ShippingRegions { get; set; }
        public List<TaxResponse> Taxes { get; set; }
    }
}
