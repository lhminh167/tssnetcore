namespace Tss.FunctionalTests.Core.Models
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

        public bool Shipping { get; set; }
        public bool ShippingRegion { get; set; }
        public bool Tax { get; set; }
        public bool Role { get; set; }
    }
}