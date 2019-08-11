using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class ProductAttribute
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int AttributeValueId { get; set; }
    }
}
