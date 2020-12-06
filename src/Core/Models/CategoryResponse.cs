using System.Collections.Generic;

namespace Tss.Core.Models
{
    public class CategoryResponse
    {
        public CategoryResponse()
        {
            ProductCategory = new HashSet<ProductCategoryResponse>();
        }

        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual DepartmentResponse Department { get; set; }
        public virtual ICollection<ProductCategoryResponse> ProductCategory { get; set; }
    }
}
