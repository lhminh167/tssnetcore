using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Sieve.Attributes;

namespace Tss.Core.Entities
{
    public class ProductAttribute
    {
        public ProductAttribute(Product product, AttributeValue attributeValue)
        {
            Guard.Against.Null(product, nameof(product));
            Guard.Against.Null(attributeValue, nameof(attributeValue));

            ProductId = product.ProductId;
            AttributeValueId = attributeValue.AttributeValueId;

            Product = product;
            AttributeValue = attributeValue;
        }

        public ProductAttribute()
        {
        }

        public int ProductId { get; set; }
        public int AttributeValueId { get; set; }

        public virtual AttributeValue AttributeValue { get; set; }
        public virtual Product Product { get; set; }
    }
}
