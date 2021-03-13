using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Models
{
    public class ShippingRegion
    {
        public ShippingRegion()
        {
            Customer = new HashSet<Customer>();
            Shipping = new HashSet<Shipping>();
        }

        public int ShippingRegionId { get; set; }
        public string ShippingRegion1 { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Shipping> Shipping { get; set; }
    }
}
