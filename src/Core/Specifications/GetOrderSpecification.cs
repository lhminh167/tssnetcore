using Tss.Core.Entities;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetOrderSpecification : BaseSpecification<Orders, Orders>
    {
        public GetOrderSpecification(IAuthHelper authHelper, PaginationRequest input)
        : base(
            o => o.CustomerId == authHelper.GetCustomerId() || authHelper.GetRole() == "Admin"
            ,
            o => o
        )
        {
            SetIsNoTracking(true);
            AddInclude(o => o.Customer);
            ApplyOrderByDescending(o => o.CreatedOn);
            ApplyPaging(input.PageIndex, input.PageSize, input.IsPagingEnabled);
        }
    }

}
