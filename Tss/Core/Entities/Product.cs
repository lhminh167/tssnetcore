using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class Product
    {
        public Product(ProductCreateRequest input)
        {
            Guard.Against.Null(input, nameof(input));
            
            AddCommonInfo(input);
            AddProductAttributes(input.Attributes);
        }

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

        public virtual IReadOnlyCollection<AttributeValueResponse> Attributes => ProductAttribute?.Select(pa => new AttributeValueResponse(pa?.AttributeValue))?.ToList();

        public bool CheckAttributes(string attributeList)
        {
            Guard.Against.NullOrEmpty(attributeList, nameof(attributeList));

            IDictionary<string, string> dict = AttributeValue.Decode(attributeList);

            List<AttributeValueResponse> matchedAttributeValues = Attributes?.Where(a =>
                dict.Contains(
                    new KeyValuePair<string, string>(
                        a?.Name,
                        a?.Value
                    )
                )
            ).ToList();

            return dict.Count == matchedAttributeValues.Count;
        }

        private void AddProductAttributes(List<AttributeValue> attributeValues)
        {
            Guard.Against.NullOrEmpty(attributeValues, nameof(attributeValues));
            
            List<ProductAttribute> productAttributes = attributeValues.Select(av => new ProductAttribute(this, av)).ToList();
            ProductAttribute.ToList().AddRange(productAttributes);
        }

        private void UpdateProductAttributes(List<AttributeValue> attributeValues)
        {
            Guard.Against.Null(attributeValues, nameof(attributeValues));

            ProductAttribute.ToList().RemoveAll(pa => pa.ProductId == ProductId);
            AddProductAttributes(attributeValues);
        }

        public void UpdateInfo(ProductCreateRequest input)
        {
            Guard.Against.Null(input, nameof(input));

            AddCommonInfo(input);
            UpdateProductAttributes(input.Attributes);
        }

        private void AddCommonInfo(ProductCreateRequest input)
        {
            Guard.Against.NullOrWhiteSpace(input.Name, nameof(input.Name));
            Guard.Against.NullOrWhiteSpace(input.Description, nameof(input.Description));
            Guard.Against.BadRequest(input.Price <= 0, "Price should be greater than zero.");
            Guard.Against.BadRequest(input.DiscountedPrice <= 0, "Discounted price should be greater than zero.");
            Guard.Against.BadRequest(input.DiscountedPrice >= input.Price, "Discounted price should be less than price.");
            Guard.Against.NullOrWhiteSpace(input.Image, nameof(Image));
            Guard.Against.NullOrWhiteSpace(input.Image2, nameof(Image2));
            Guard.Against.NullOrWhiteSpace(input.Thumbnail, nameof(Thumbnail));
            Guard.Against.BadRequest(input.Display <= 0, "Display value should be greater than zero.");

            Name = input.Name;
            Description = input.Description;
            Price = input.Price;
            DiscountedPrice = input.DiscountedPrice;
            Image = input.Image;
            Image2 = input.Image2;
            Thumbnail = input.Thumbnail;
            Display = input.Display;
        }
    }
}
