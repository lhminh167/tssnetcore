using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Models
{
    public class AttributeValueResponse
    {
        public AttributeValueResponse()
        {
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
