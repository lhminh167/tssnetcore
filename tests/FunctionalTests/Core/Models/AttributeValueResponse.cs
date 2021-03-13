using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
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

        public AttributeValueResponse()
        {
            CustomerAttribute = new List<CustomerAttributeResponse>();
            ProductAttribute = new List<ProductAttributeResponse>();
        }

        public int AttributeValueId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

        public virtual AttributeResponse Attribute { get; set; }
        private List<CustomerAttributeResponse> CustomerAttribute { get; set; }
        private List<ProductAttributeResponse> ProductAttribute { get; set; }
    }
}
