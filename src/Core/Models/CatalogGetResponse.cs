using System.Collections.Generic;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class CatalogGetResponse
    {
        public CatalogGetResponse()
        {
            Shipping = new List<Shipping>();
            ShippingRegion = new List<ShippingRegion>();
            Tax = new List<Tax>();
            Role = new List<AttributeValue>();
        }

        public List<Shipping> Shipping { get; private set; }
        public List<ShippingRegion> ShippingRegion { get; private set; }
        public List<Tax> Tax { get; private set; }
        public List<AttributeValue> Role { get; private set; }

        public void SetShipping(List<Shipping> shipping)
        {
            if (shipping == null || shipping.Count == 0) return;
            Shipping = shipping;
        }

        public void SetShippingRegion(List<ShippingRegion> shippingRegion)
        {
            if (shippingRegion == null || shippingRegion.Count == 0) return;
            ShippingRegion = shippingRegion;
        }

        public void SetTax(List<Tax> tax)
        {
            if (tax == null || tax.Count == 0) return;
            Tax = tax;
        }

        public void SetRole(List<AttributeValue> role)
        {
            if (role == null || role.Count == 0) return;
            Role = role;
        }
    }
}