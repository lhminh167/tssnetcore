using System;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;
using Tss.Core.Interfaces;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ReviewResponse
    {
        public ReviewResponse()
        {
        }

        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Review1 { get; set; }
        public short Rating { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual CustomerResponse Customer { get; set; }
        public virtual ProductResponse Product { get; set; }
    }
}
