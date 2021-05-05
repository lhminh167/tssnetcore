using System.Collections.Generic;
using Tss.FunctionalTests.Core.Entities;

namespace Tss.FunctionalTests.Core.Models
{
    public class ProductResponse
    {

        public ProductResponse()
        {
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Thumbnail { get; set; }
        public short Display { get; set; }

        public virtual ReviewGetResponse Review { get; set; }
        public virtual List<AttributeValueResponse> Attributes { get; set; }
        public virtual ProductFilterCategoryResponse Category { get; set; }
        public virtual PaginatedList<ProductResponse> Recommendation { get; set; }
        public virtual List<OrderDetail> OrderDetail { get; set; }
    }
}
