using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Review
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ReviewId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int CustomerId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Review1 { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public short Rating { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedOn { get; set; }
    }
}
