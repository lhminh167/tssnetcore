using System;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Models
{
    public class ReviewResponse
    {
        public ReviewResponse(Review input)
        {
            if (input is null) return;

            ReviewId = input.ReviewId;
            CustomerId = input.CustomerId;
            ProductId = input.ProductId;
            Review1 = input.Review1;
            Rating = input.Rating;
            CreatedOn = input.CreatedOn;

            Customer = new CustomerResponse(input.Customer);
            Product = new ProductResponse(input.Product);
        }

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
