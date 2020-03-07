using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetOrderSpecification : BaseSpecification<Orders, Orders>
    {
        public GetOrderSpecification(IAuthHelper authHelper)
        : base(
            o => o.CustomerId == authHelper.GetCustomerId() || authHelper.GetRole() == "Admin"
            ,
            o => o
        )
        {
            SetIsNoTracking(true);
            // AddInclude(o => o.Tax);
            // AddInclude(o => o.Shipping);
            AddInclude(o => o.Customer);
            // AddInclude(o => o.OrderDetail);
            // AddInclude(o => o.Audit);
            ApplyOrderByDescending(o => o.CreatedOn);
        }
    }

}
