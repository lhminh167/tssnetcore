using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetReviewByProductIdSpecification : BaseSpecification<Review, Review>
    {
        public GetReviewByProductIdSpecification(int productId, IAuthHelper authHelper)
        : base(
            r 
            => r.ProductId == productId
            ,
            p => p
        )
        {
            SetIsNoTracking(true);
            AddInclude(r => r.Customer);
        }
    }
}
