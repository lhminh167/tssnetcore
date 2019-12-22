using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetCustomerByIdSpecification : BaseSpecification<Customer, Customer>
    {
        public GetCustomerByIdSpecification(int customerId, IAuthHelper authHelper)
        : base(
            c
            => c.CustomerId == customerId
            && (c.CustomerId == authHelper.GetCustomerId() || authHelper.GetRole() == "Admin")
            ,
            c => c
        )
        {
            AddInclude("CustomerAttribute.AttributeValue.Attribute");
            AddInclude(c => c.ShippingRegion);
        }
    }
}
