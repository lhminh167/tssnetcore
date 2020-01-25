using Tss.Core.Entities;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Tss.Core.Specifications;
using Tss.Core.Models;

namespace Tss.Core.Services
{
    public partial class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Review> _reviewRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IAuthHelper _authHelper;

        public ReviewService(
            IUnitOfWork unitOfWork,
            IRepository<Review> reviewRepo,
            IRepository<Product> productRepo,
            IRepository<Customer> customerRepo,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _reviewRepo = reviewRepo ?? throw new System.ArgumentNullException(nameof(reviewRepo));
            _productRepo = productRepo ?? throw new System.ArgumentNullException(nameof(productRepo));
            _customerRepo = customerRepo ?? throw new System.ArgumentNullException(nameof(customerRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        public async Task<List<Review>> Get(int productId)
        {
            return await _reviewRepo.Get(new GetReviewByProductIdSpecification(productId, _authHelper));
        }

        public async Task<Review> GetOne(int reviewId)
        {
            return await _reviewRepo.GetOne(new GetReviewByIdSpecification(reviewId, _authHelper));
        }

        public async Task<Review> Create(ReviewCreateRequest input)
        {
            Product product = await _productRepo.Get(input.ProductId);
            Guard.Against.NotFound(product, nameof(Product));

            Customer customer = await _customerRepo.Get(input.CustomerId);
            Guard.Against.NotFound(customer, nameof(Customer));

            Review review = new Review(input, _authHelper);
            _reviewRepo.Post(review);
            await _unitOfWork.SaveAsync();
            return review;
        }

        public async Task<Review> Update(Review input)
        {
            Review review = await _reviewRepo.GetOne(new GetReviewByIdSpecification(input.ReviewId, _authHelper));
            Guard.Against.NotFound(review, nameof(Review));

            review.Update(input, _authHelper);
            _reviewRepo.Put(review);
            await _unitOfWork.SaveAsync();
            return review;
        }

        public async Task<Review> Delete(int reviewId)
        {
            Review review = await _reviewRepo.GetOne(new GetReviewByIdSpecification(reviewId, _authHelper));
            Guard.Against.NotFound(review, nameof(Review));

            bool isAuthorized = 
                _authHelper.GetCustomerId() == review.CustomerId
                || _authHelper.GetRole() == "Admin";
            Guard.Against.Unauthorized(!isAuthorized);

            _reviewRepo.Delete(review);
            await _unitOfWork.SaveAsync();
            return review;
        }
    }
}