using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetCustomerByIdWithReviewsSpecification : BaseSpecification<Customer, Customer>
    {
        public GetCustomerByIdWithReviewsSpecification(IAuthHelper authHelper)
        : base(
            c
            => c.CustomerId == authHelper.GetCustomerId()
            ,
            c => c
        )
        {
            SetIsNoTracking(true);
            AddInclude(c => c.Review);
        }
    }
}
