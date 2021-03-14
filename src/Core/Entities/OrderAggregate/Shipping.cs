using System.Collections.Generic;

namespace Tss.Core.Entities
{
    public class Shipping
    {
        public Shipping()
        {
            Orders = new HashSet<Orders>();
        }

        public int ShippingId { get; private set; }
        public string ShippingType { get; private set; }
        public double ShippingCost { get; private set; }
        public int ShippingRegionId { get; private set; }

        public virtual ShippingRegion ShippingRegion { get; private set; }
        public virtual ICollection<Orders> Orders { get; private set; }
    }
}
