using System.Collections.Generic;
using Sieve.Attributes;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ShoppingCartItemAdd
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int ItemId { get; set; }
                
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Attributes { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int Quantity { get; set; }
    }

}
