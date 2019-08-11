using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class OrderDetail
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ItemId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int OrderId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Attributes { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string ProductName { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int Quantity { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal UnitCost { get; set; }
    }
}
