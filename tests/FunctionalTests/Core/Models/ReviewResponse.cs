using System;
using Tss.FunctionalTests.Core.Entities;

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
        public virtual Product Product { get; set; }
    }
}
