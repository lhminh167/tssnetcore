using System;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tss.Core.Entities
{
    public class ProductCategory
    {
        public ProductCategory(int productId, int categoryId)
        {
            // Guard.Against.Null(product, nameof(Product));
            // Guard.Against.Null(category, nameof(Category));

            ProductId = productId;
            CategoryId = categoryId;

            // Product = product;
            // Category = category;
        }

        public ProductCategory()
        {
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
