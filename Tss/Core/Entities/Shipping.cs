using System.Collections.Generic;

namespace Tss.Core.Entities
{
    public class Shipping
    {
        public Shipping()
        {
            Orders = new HashSet<Orders>();
        }

        public int ShippingId { get; set; }
        public string ShippingType { get; set; }
        public double ShippingCost { get; set; }
        public int ShippingRegionId { get; set; }

        public virtual ShippingRegion ShippingRegion { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
