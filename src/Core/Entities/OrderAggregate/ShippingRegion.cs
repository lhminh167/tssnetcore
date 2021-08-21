using System.Collections.Generic;

namespace Tss.Core.Entities
{
    public class ShippingRegion
    {
        private ShippingRegion()
        {
            Customer = new HashSet<Customer>();
            Shipping = new HashSet<Shipping>();
        }

        public int ShippingRegionId { get; private set; }
        public string ShippingRegion1 { get; private set; }

        public virtual ICollection<Customer> Customer { get; private set; }
        public virtual ICollection<Shipping> Shipping { get; private set; }
    }
}
