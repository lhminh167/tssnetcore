using System.Collections.Generic;

namespace Tss.Core.Entities
{
    public class ProductAttribute
    {
        public ProductAttribute(int productId, int attributeValueId)
        {
            ProductId = productId;
            AttributeValueId = attributeValueId;
        }

        public ProductAttribute()
        {
        }

        public int ProductId { get; set; }
        public int AttributeValueId { get; set; }

        public virtual AttributeValue AttributeValue { get; set; }
        public virtual Product Product { get; set; }

        public static IReadOnlyList<string> AllowedValues => new List<string> { "Color", "Size" };
    }
}
