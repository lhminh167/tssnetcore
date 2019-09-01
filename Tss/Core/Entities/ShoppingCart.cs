using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Sieve.Attributes;
using System.Linq;

namespace Tss.Core.Entities
{
    public class ShoppingCart
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)]
        public int ItemId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int CartId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Attributes { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int Quantity { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public bool? BuyNow { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime AddedOn { get; set; }

        public List<string> DecodeAttributes() {
            return Attributes.Split(":").LastOrDefault().Split("/").ToList();
        }
    }
}
