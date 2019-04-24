using System;
using System.Collections.Generic;

namespace Tss.Core
{
    public partial class Shipping
    {
        public int ShippingId { get; set; }
        public string ShippingType { get; set; }
        public decimal ShippingCost { get; set; }
        public int ShippingRegionId { get; set; }
    }
}
