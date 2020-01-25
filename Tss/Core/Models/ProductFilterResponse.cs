using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductFilterResponse
    {
        public ProductFilterResponse(List<AttributeValue> attributes, List<ProductFilterCategoryResponse> categories)
        {
            Guard.Against.Null(attributes, nameof(attributes));
            Guard.Against.Null(categories, nameof(categories));
            Guard.Against.OutOfRange(attributes.Count, nameof(attributes.Count), 1, int.MaxValue);
            Guard.Against.OutOfRange(categories.Count, nameof(categories.Count), 1, int.MaxValue);

            Attributes = attributes.Select(av => new AttributeValueResponse(av)).ToList();
            Categories = categories;
        }

        public List<AttributeValueResponse> Attributes { get; set; }
        public List<ProductFilterCategoryResponse> Categories { get; set; }
    }
}
