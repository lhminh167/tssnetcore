using System;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class Review
    {
        public Review(ReviewCreateRequest input, Product product, Customer customer, IAuthHelper authHelper)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.Null(product, nameof(product));
            Guard.Against.Null(customer, nameof(customer));
            Guard.Against.Null(authHelper, nameof(authHelper));
            // Guard.Against.BadRequest(
            //     customer.Review.Any(r => r.ProductId == product.ProductId),
            //     "You already reviewed this product."
            // );
            Guard.Against.NullOrWhiteSpace(input.Review1, "Review content");
            Guard.Against.OutOfRange(input.Review1.Length, "Review content", 50, int.MaxValue);
            Guard.Against.OutOfRange(input.Rating, nameof(Rating), 1, 5);

            CustomerId = customer.CustomerId;
            ProductId = product.ProductId;
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

        public void Update(ReviewUpdateRequest input, IAuthHelper authHelper)
        {
            Guard.Against.Unauthorized(authHelper.GetCustomerId() != CustomerId);
            Guard.Against.NullOrWhiteSpace(input.Review1, "Review content");
            Guard.Against.OutOfRange(input.Review1.Length, "Review content", 50, int.MaxValue);
            Guard.Against.OutOfRange(input.Rating, nameof(Rating), 1, 5);

            Review1 = input.Review1;
            Rating = input.Rating;
            CreatedOn = DateTime.Now;
        }

        public void Delete(IAuthHelper authHelper)
        {
            bool isAuthorized =
                authHelper.GetCustomerId() == CustomerId
                || authHelper.GetRole() == "Admin";

            Guard.Against.Unauthorized(!isAuthorized);
        }

        public void SetCustomer(Customer customer)
        {
            Guard.Against.Null(customer, nameof(customer));

            Customer = customer;
        }

    }
}
