using System;
using System.Collections.Generic;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class Department
    {
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int DepartmentId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }
    }
}
