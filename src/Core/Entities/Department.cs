using System.Collections.Generic;

namespace Tss.Core.Entities
{
    public class Department
    {
        private Department()
        {
            Category = new HashSet<Category>();
        }

        public int DepartmentId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual ICollection<Category> Category { get; private set; }
    }
}
