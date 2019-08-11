using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Audit
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int AuditId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int OrderId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedOn { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Message { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int Code { get; set; }
    }
}
