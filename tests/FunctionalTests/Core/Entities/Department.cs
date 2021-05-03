using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Entities
{
    public class Department
    {
        public Department()
        {
            Category = new HashSet<Category>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Category> Category { get; set; }
    }
}
