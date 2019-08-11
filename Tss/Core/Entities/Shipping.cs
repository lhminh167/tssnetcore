using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Shipping
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ShippingId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string ShippingType { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal ShippingCost { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ShippingRegionId { get; set; }
    }
}
