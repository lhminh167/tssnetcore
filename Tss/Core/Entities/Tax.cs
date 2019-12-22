using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Tax
    {
        public Tax()
        {
            Orders = new HashSet<Orders>();
        }

        public int TaxId { get; set; }
        public string TaxType { get; set; }
        public double TaxPercentage { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
