using System.Collections.Generic;
using System.Linq;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class CatalogGetResponse
    {
        public CatalogGetResponse()
        {
            Shipping = new List<ShippingResponse>();
            ShippingRegion = new List<ShippingRegionResponse>();
            Tax = new List<TaxResponse>();
            Role = new List<AttributeValueResponse>();
        }

        public List<ShippingResponse> Shipping { get; set; }
        public List<ShippingRegionResponse> ShippingRegion { get; set; }
        public List<TaxResponse> Tax { get; set; }
        public List<AttributeValueResponse> Role { get; set; }

        public void SetShipping(List<Shipping> shipping)
        {
            if (shipping == null || shipping.Count == 0) return;
            Shipping = shipping.Select(e => new ShippingResponse(e)).ToList();
        }

        public void SetShippingRegion(List<ShippingRegion> shippingRegion)
        {
            if (shippingRegion == null || shippingRegion.Count == 0) return;
            ShippingRegion = shippingRegion.Select(e => new ShippingRegionResponse(e)).ToList();
        }

        public void SetTax(List<Tax> tax)
        {
            if (tax == null || tax.Count == 0) return;
            Tax = tax.Select(e => new TaxResponse(e)).ToList();
        }

        public void SetRole(List<AttributeValue> role)
        {
            if (role == null || role.Count == 0) return;
            Role = role.Select(e => new AttributeValueResponse(e)).ToList();
        }
    }
}