using Tss.Core.Entities;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
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

        public async Task<Review> GetOne(int reviewId)
        {
            var review = await _reviewRepo.GetOne(new GetReviewByIdSpecification(reviewId, _authHelper));
            Guard.Against.NotFound(review, nameof(Review));

            return review;
        }

        public async Task<Review> Create(ReviewCreateRequest input)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByIdWithReviewsSpecification(_authHelper));
            Guard.Against.NotFound(customer, nameof(Customer));

            Product product = await _productRepo.Get(input.ProductId);
            Guard.Against.NotFound(product, nameof(Product));

            Review review = new Review(input, product, customer, _authHelper);
            _reviewRepo.Post(review);
            await _unitOfWork.SaveAsync();
            review.SetCustomer(customer);
            return review;
        }

        public async Task<Review> Update(ReviewUpdateRequest input)
        {
            Review review = await _reviewRepo.Get(input.ReviewId);
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

            review.Delete(_authHelper);
            _reviewRepo.Delete(review);
            await _unitOfWork.SaveAsync();
            return review;
        }
    }
}