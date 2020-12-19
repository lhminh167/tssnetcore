using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetReviewByIdSpecification : BaseSpecification<Review, Review>
    {
        public GetReviewByIdSpecification(int reviewId, IAuthHelper authHelper)
        : base(
            r 
            => r.ReviewId == reviewId
            ,
            r => r
        )
        {
            SetIsNoTracking(true);
            AddInclude(r => r.Customer);
        }
    }
}
