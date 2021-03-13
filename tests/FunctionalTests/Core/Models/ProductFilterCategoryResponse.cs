using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ProductFilterCategoryResponse
    {
        public ProductFilterCategoryResponse(Category category)
        {
            Guard.Against.Null(category, nameof(category));

            CategoryId = category.CategoryId;
            Name = category.Name;
        }

        public ProductFilterCategoryResponse()
        { }

        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

}
