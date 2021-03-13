using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Models
{
    public class Category
    {
        public Category()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
