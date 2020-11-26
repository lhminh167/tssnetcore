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

        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public double DiscountedPrice { get; private set; }
        public string Image { get; private set; }
        public string Image2 { get; private set; }
        public string Thumbnail { get; private set; }
        public short Display { get; private set; }

        public virtual ReviewGetResponse Review { get; private set; }
        public virtual IReadOnlyCollection<AttributeValueResponse> Attributes { get; private set; }
        public virtual ProductFilterCategoryResponse Category { get; private set; }
        public virtual PaginatedList<Product> Recommendation { get; private set; }
    }
}
