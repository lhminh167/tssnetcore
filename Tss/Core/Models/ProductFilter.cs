using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductFilter
    {
        public ProductFilter(List<AttributeValue> attributeValues, List<ProductFilterCategoryResponse> categories)
        {
            Guard.Against.Null(attributeValues, nameof(attributeValues));
            Guard.Against.Null(categories, nameof(categories));
            Guard.Against.OutOfRange(attributeValues.Count, nameof(attributeValues.Count), 1, int.MaxValue);
            Guard.Against.OutOfRange(categories.Count, nameof(categories.Count), 1, int.MaxValue);

            AttributeValues = attributeValues.Select(av => new AttributeValueResponse(av)).ToList();
            Categories = categories;
        }

        public List<AttributeValueResponse> AttributeValues { get; set; }
        public List<ProductFilterCategoryResponse> Categories { get; set; }
    }
}
