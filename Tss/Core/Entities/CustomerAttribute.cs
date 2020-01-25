using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace Tss.Core.Entities
{
    public partial class CustomerAttribute
    {
        public CustomerAttribute(Customer customer, AttributeValue attributeValue)
        {
            Guard.Against.Null(customer, nameof(customer));
            Guard.Against.Null(attributeValue, nameof(attributeValue));
            Guard.Against.BadRequest(AllowedValues.Contains(attributeValue.Attribute.Name), $"{attributeValue.Attribute.Name} is not a customer's attribute.");

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

        public static IReadOnlyList<string> AllowedValues => new List<string>{"Role"};
    }
}
