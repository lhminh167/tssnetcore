using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Models
{
    public class ProductAttributeResponse
    {
        public ProductAttributeResponse(int productId, int attributeValueId)
        {
            ProductId = productId;
            AttributeValueId = attributeValueId;
        }

        public ProductAttributeResponse()
        {
        }

        public int ProductId { get; set; }
        public int AttributeValueId { get; set; }

        public virtual AttributeValueResponse AttributeValue { get; set; }
        public virtual ProductResponse Product { get; set; }

        public static List<string> AllowedAttributeNames => new List<string> { "Color", "Size" };
    }
}
