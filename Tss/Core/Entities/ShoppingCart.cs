using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class ShoppingCart
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ItemId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string CartId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Attributes { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int Quantity { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public sbyte BuyNow { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime AddedOn { get; set; }
    }
}
