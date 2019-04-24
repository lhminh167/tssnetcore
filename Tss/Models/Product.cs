using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Models
{
    public partial class Product
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal Price { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal DiscountedPrice { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Image { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Image2 { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Thumbnail { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public short Display { get; set; }
    }
}
