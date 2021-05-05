using System;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Entities
{
    public class Review
    {
        public Review()
        {
        }

        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Review1 { get; set; }
        public short Rating { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

    }
}
