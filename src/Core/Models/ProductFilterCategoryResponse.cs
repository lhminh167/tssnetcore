using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductFilterCategoryResponse
    {
        public ProductFilterCategoryResponse(Category category)
        {
            Guard.Against.Null(category, nameof(category));

            CategoryId = category.CategoryId;
            Name = category.Name;
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

}
