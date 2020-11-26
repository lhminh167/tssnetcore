namespace Tss.Core.Models
{
    public class CatalogGetRequest
    {
        public CatalogGetRequest(
            bool shipping,
            bool shippingRegion,
            bool tax,
            bool role
        )
        {
            Shipping = shipping;
            ShippingRegion = shippingRegion;
            Tax = tax;
            Role = role;
        }

        public bool Shipping { get; private set; }
        public bool ShippingRegion { get; private set; }
        public bool Tax { get; private set; }
        public bool Role { get; private set; }
    }
}