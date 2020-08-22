using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductResponse
    {
        public ProductResponse(
            Product input
        )
        {
            Guard.Against.Null(input, nameof(input));

            ProductId = input.ProductId;
            Name = input.Name;
            Description = input.Description;
            Price = input.Price;
            DiscountedPrice = input.DiscountedPrice;
            Image = input.Image;
            Image2 = input.Image2;
            Thumbnail = input.Thumbnail;
            Display = input.Display;
        }

        public ProductResponse(
            Product input,
            ReviewGetResponse review,
            PaginatedList<Product> recommedation
        )
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.Null(review, nameof(review));
            Guard.Against.Null(recommedation, nameof(recommedation));

            ProductId = input.ProductId;
            Review = review;
            Name = input.Name;
            Description = input.Description;
            Price = input.Price;
            DiscountedPrice = input.DiscountedPrice;
            Image = input.Image;
            Image2 = input.Image2;
            Thumbnail = input.Thumbnail;
            Display = input.Display;
            Attributes = input.ProductAttribute.Select(pa => new AttributeValueResponse(pa.AttributeValue)).ToList();
            Category = input.ProductCategory.Select(pc => new ProductFilterCategoryResponse(pc.Category)).FirstOrDefault();
            Recommendation = recommedation;
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

        public virtual ReviewGetResponse Review { get; set; }
        public virtual IReadOnlyCollection<AttributeValueResponse> Attributes { get; set; }
        public virtual ProductFilterCategoryResponse Category { get; set; }
        public virtual PaginatedList<Product> Recommendation { get; set; }
    }
}
