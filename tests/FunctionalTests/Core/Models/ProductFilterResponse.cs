using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ProductFilterResponse
    {
        public ProductFilterResponse()
        {
        }

        public List<AttributeValueResponse> Attributes { get; set; }
        public List<ProductFilterCategoryResponse> Categories { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
    }
}
