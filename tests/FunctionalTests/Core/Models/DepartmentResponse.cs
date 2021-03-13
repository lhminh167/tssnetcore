using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Models
{
    public class DepartmentResponse
    {
        public DepartmentResponse()
        {
            Category = new List<CategoryResponse>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<CategoryResponse> Category { get; set; }
    }
}
