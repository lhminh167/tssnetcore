using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Customer
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int CustomerId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Email { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Password { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string CreditCard { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Address1 { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Address2 { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string City { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Region { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string PostalCode { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Country { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ShippingRegionId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string DayPhone { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string EvePhone { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string MobPhone { get; set; }
    }
}
