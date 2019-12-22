using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Extensions;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
            ProductAttribute = new HashSet<ProductAttribute>();
            ProductCategory = new HashSet<ProductCategory>();
            Review = new HashSet<Review>();
            ShoppingCart = new HashSet<ShoppingCart>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Thumbnail { get; set; }
        public short Display { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }

        // private IReadOnlyCollection<AttributeValueResponse> GetAttributes()
        // {
        //     return ProductAttribute.Select(pa => new AttributeValueResponse(pa.AttributeValue)).ToList();
        // }

        public bool CheckAttributes(string attributeList)
        {
            IDictionary<string, string> dict = AttributeValue.Decode(attributeList);

            IEnumerable<ProductAttribute> matchedAttributeValues = ProductAttribute.Where(pa =>
                dict.Contains(
                    new KeyValuePair<string, string>(
                        pa.AttributeValue.Attribute.Name,
                        pa.AttributeValue.Value
                    )
                )
            );

            return dict.Count == matchedAttributeValues.Count();
        }

        private void AddProductAttribute(AttributeValue attributeValue)
        {
            Guard.Against.Null(attributeValue, nameof(attributeValue));

            ProductAttribute.Add(new ProductAttribute(this, attributeValue));
        }

        public void UpdateProductAttribute(ProductAttribute currentProductAttribute, AttributeValue attributeValueToAdd)
        {
            Guard.Against.Null(currentProductAttribute, nameof(currentProductAttribute));
            Guard.Against.Null(attributeValueToAdd, nameof(attributeValueToAdd));

            ProductAttribute.Remove(currentProductAttribute);
            AddProductAttribute(attributeValueToAdd);
        }
    }
}
