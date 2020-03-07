using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductResponse
    {
        public ProductResponse(
            Product input,
            bool hideAttributes = false
        )
        {
            Guard.Against.Null(input, nameof(input));

            ProductId = input.ProductId;
            Review = input.Review;
            Name = input.Name;
            Description = input.Description;
            Price = input.Price;
            DiscountedPrice = input.DiscountedPrice;
            Image = input.Image;
            Image2 = input.Image2;
            Thumbnail = input.Thumbnail;
            Display = input.Display;
            if (hideAttributes) return;
            Attributes = input.ProductAttribute?.Select(pa => new AttributeValueResponse(pa?.AttributeValue))?.ToList();
            Category = input.ProductCategory?.Select(pc => new ProductFilterCategoryResponse(pc?.Category))?.FirstOrDefault();
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

        // public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        // public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
        // public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        // public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }


        public virtual IReadOnlyCollection<AttributeValueResponse> Attributes { get; set; }
        public virtual ProductFilterCategoryResponse Category { get; set; }
    }
}
