using System.Collections.Generic;
using Tss.Core.Entities;

namespace Tss.Core.ProductFilters
{
    public class ProductFiltersFilters
    {
        public List<AttributeValue> Colors { get; set; }
        public List<AttributeValue> Sizes { get; set; }
        public List<Category> Categories { get; set; }
    }
}
