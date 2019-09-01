using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class AttributeValue
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int AttributeValueId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int AttributeId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Value { get; set; }
    }
}
