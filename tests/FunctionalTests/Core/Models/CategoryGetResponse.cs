using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class CategoryGetResponse
    {
        public CategoryGetResponse()
        {
        }

        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual PaginatedList<ProductResponse> Product { get; set; }
    }
}
