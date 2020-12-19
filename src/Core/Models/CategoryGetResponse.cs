using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class CategoryGetResponse
    {
        public CategoryGetResponse(
            Category category,
            PaginatedList<Product> product
        )
        {
            Guard.Against.Null(category, nameof(category));
            Guard.Against.Null(product, nameof(product));

            CategoryId = category.CategoryId;
            Name = category.Name;
            Description = category.Description;
            Product = product;
        }

        public int CategoryId { get; set; }
        [DefaultValue(1)]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual PaginatedList<Product> Product { get; set; }
    }
}
