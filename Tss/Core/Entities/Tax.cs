using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Tax
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int TaxId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string TaxType { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal TaxPercentage { get; set; }
    }
}
