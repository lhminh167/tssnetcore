using System;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Models
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

        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Review1 { get; set; }
        public short Rating { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

        public void Update(ReviewUpdateRequest input, IAuthHelper authHelper)
        {
            Allow(authHelper);
            Guard.Against.NullOrWhiteSpace(input.Review1, "Review content");
            Guard.Against.OutOfRange(input.Review1.Length, "Review content", 50, int.MaxValue);
            Guard.Against.OutOfRange(input.Rating, nameof(Rating), 1, 5);

            Review1 = input.Review1;
            Rating = input.Rating;
            CreatedOn = DateTime.Now;
        }

        public void Delete(IAuthHelper authHelper)
        {
            Allow(authHelper);
        }

        public void SetCustomer(Customer customer)
        {
            Guard.Against.Null(customer, nameof(customer));

            Customer = customer;
        }

        private void Allow(IAuthHelper authHelper)
        {
            bool isAuthorized =
                authHelper.GetCustomerId() == CustomerId
                || authHelper.GetRole() == Role.Admin.Message;

            Guard.Against.Unauthorized(!isAuthorized);
        }
    }
}
