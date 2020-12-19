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

        public int ProductId { get; private set; }
        public int AttributeValueId { get; private set; }

        public virtual AttributeValue AttributeValue { get; private set; }
        public virtual Product Product { get; private set; }

        public static IReadOnlyList<string> AllowedValues => new List<string> { "Color", "Size" };
    }
}
