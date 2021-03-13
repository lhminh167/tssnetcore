using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Models
{
    public class CategoryResponse
    {
        public CategoryResponse()
        {
            ProductCategory = new List<ProductCategoryResponse>();
        }

        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual DepartmentResponse Department { get; set; }
        public virtual List<ProductCategoryResponse> ProductCategory { get; set; }
    }
}
