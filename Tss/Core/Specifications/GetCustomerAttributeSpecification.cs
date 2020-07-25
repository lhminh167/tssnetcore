using Tss.Core.Entities;

namespace Tss.Core.Specifications
{
    public class GetCustomerAttributeSpecification : BaseSpecification<CustomerAttribute, CustomerAttribute>
    {
        public GetCustomerAttributeSpecification(int customerId, string attributeName)
        : base(
            c => c.CustomerId == customerId
            && c.AttributeValue.Attribute.Name == attributeName,
            c => c
        )
        {
            SetIsNoTracking(true);
        }
    }
}
