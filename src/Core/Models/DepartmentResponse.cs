using System.Collections.Generic;

namespace Tss.Core.Models
{
    public class DepartmentResponse
    {
        public DepartmentResponse()
        {
            Category = new HashSet<CategoryResponse>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CategoryResponse> Category { get; set; }
    }
}
