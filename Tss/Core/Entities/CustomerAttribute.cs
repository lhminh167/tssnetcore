using Ardalis.GuardClauses;

namespace Tss.Core.Entities
{
    public partial class CustomerAttribute
    {
        public CustomerAttribute(Customer customer, AttributeValue attributeValue)
        {
            Guard.Against.Null(customer, nameof(customer));
            Guard.Against.Null(attributeValue, nameof(attributeValue));

            CustomerId = customer.CustomerId;
            AttributeValueId = attributeValue.AttributeValueId;

            Customer = customer;
            AttributeValue = attributeValue;
        }

        private CustomerAttribute()
        {
        }

        public int CustomerId { get; set; }
        public int AttributeValueId { get; set; }

        public virtual AttributeValue AttributeValue { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
