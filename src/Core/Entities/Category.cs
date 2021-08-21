using System.Collections.Generic;

namespace Tss.Core.Entities
{
    public class Category
    {
        private Category()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; private set; }
        public int DepartmentId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual Department Department { get; private set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; private set; }
    }
}
