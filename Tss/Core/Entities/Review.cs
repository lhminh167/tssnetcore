using System;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class Review
    {
        public Review(ReviewCreateRequest input, IAuthHelper authHelper)
        {
            Guard.Against.Null(authHelper, nameof(authHelper));
            Guard.Against.OutOfRange(input.CustomerId, nameof(CustomerId), 1, int.MaxValue);
            Guard.Against.OutOfRange(input.ProductId, nameof(ProductId), 1, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(input.Review1, "Review content");
            Guard.Against.OutOfRange(input.Rating, nameof(Rating), 1, 5);

            CustomerId = input.CustomerId;
            ProductId = input.ProductId;
            Review1 = input.Review1;
            Rating = input.Rating;
            CreatedOn = DateTime.Now;
        }

        public Review()
        {
        }

        public int ReviewId { get; private set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
        public string Review1 { get; private set; }
        public short Rating { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public virtual Customer Customer { get; private set; }
        public virtual Product Product { get; private set; }

        public void Update(Review input, IAuthHelper authHelper)
        {
            Guard.Against.Unauthorized(authHelper.GetCustomerId() != CustomerId);
            Guard.Against.NullOrWhiteSpace(input.Review1, "Review content");
            Guard.Against.OutOfRange(input.Rating, nameof(Rating), 1, 5);

            Review1 = input.Review1;
            Rating = input.Rating;
            CreatedOn = DateTime.Now;
        }
    }
}
