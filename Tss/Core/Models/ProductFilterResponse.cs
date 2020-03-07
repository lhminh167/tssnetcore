using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductFilterResponse
    {
        public ProductFilterResponse(
            List<AttributeValue> attributes,
            List<ProductFilterCategoryResponse> categories,
            double maxPrice,
            double minPrice
        )
        {
            Guard.Against.Null(attributes, nameof(attributes));
            Guard.Against.Null(categories, nameof(categories));
            Guard.Against.OutOfRange(attributes.Count, nameof(attributes.Count), 1, int.MaxValue);
            Guard.Against.OutOfRange(categories.Count, nameof(categories.Count), 1, int.MaxValue);
            Guard.Against.BadRequest(maxPrice < 0.0, "Max Price should be greater than zero.");
            Guard.Against.BadRequest(maxPrice < 0.0, "Min Price should be greater than zero.");
            Guard.Against.BadRequest(minPrice > maxPrice, "Max Price should be greater than Min Price.");

            Attributes = attributes.Select(av => new AttributeValueResponse(av)).ToList();
            Categories = categories;
            MaxPrice = maxPrice;
            MinPrice = minPrice;
        }

        public List<AttributeValueResponse> Attributes { get; private set; }
        public List<ProductFilterCategoryResponse> Categories { get; private set; }
        public double MaxPrice { get; private set; }
        public double MinPrice { get; private set; }
    }
}
