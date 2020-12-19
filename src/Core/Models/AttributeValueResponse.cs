using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Models
{
    public class AttributeValueResponse
    {
        public AttributeValueResponse(AttributeValue attributeValue)
        {
            Guard.Against.Null(attributeValue, nameof(AttributeValue));

            AttributeValueId = attributeValue.AttributeValueId;
            AttributeId = attributeValue.AttributeId;
            Value = attributeValue.Value;
            Name = attributeValue?.Attribute?.Name;
        }

        private AttributeValueResponse()
        {
            CustomerAttribute = new HashSet<CustomerAttributeResponse>();
            ProductAttribute = new HashSet<ProductAttributeResponse>();
        }

        public int AttributeValueId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

        public virtual AttributeResponse Attribute { get; set; }
        private ICollection<CustomerAttributeResponse> CustomerAttribute { get; set; }
        private ICollection<ProductAttributeResponse> ProductAttribute { get; set; }
    }
}
