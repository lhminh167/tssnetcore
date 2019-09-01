using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Attribute
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int AttributeId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
    }
}
