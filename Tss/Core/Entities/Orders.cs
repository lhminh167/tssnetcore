using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Orders
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int OrderId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal TotalAmount { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedOn { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime? ShippedOn { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int Status { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Comments { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int? CustomerId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string AuthCode { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Reference { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int? ShippingId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int? TaxId { get; set; }
    }
}
