using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace Tss.Core.Entities
{
    public class ProductAttribute
    {
        public ProductAttribute(int productId, int attributeValueId)
        {
            // Guard.Against.Null(product, nameof(product));
            // Guard.Against.Null(attributeValue, nameof(attributeValue));
            // Guard.Against.BadRequest(!AllowedValues.Contains(attributeValue.Attribute.Name), $"{attributeValue.Attribute.Name} is not a product's attribute.");

            ProductId = productId;
            AttributeValueId = attributeValueId;

            // Product = product;
            // AttributeValue = attributeValue;
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
