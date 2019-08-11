using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class ShippingRegion
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ShippingRegionId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string ShippingRegion1 { get; set; }
    }
}
