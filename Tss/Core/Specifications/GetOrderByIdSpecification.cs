using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetOrderByIdSpecification : BaseSpecification<Orders, Orders>
    {
        public GetOrderByIdSpecification(int orderId, IAuthHelper authHelper)
        : base(
            o => o.OrderId == orderId
            && (o.CustomerId == authHelper.GetCustomerId() || authHelper.GetRole() == "Admin")
            ,
            o => o
        )
        {
            SetIsNoTracking(true);
            AddInclude(o => o.Tax);
            AddInclude("Shipping.ShippingRegion");
            AddInclude(o => o.Customer);
            AddInclude(o => o.OrderDetail);
            AddInclude(o => o.Audit);
        }
    }

}
