using System.Collections.Generic;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Thumbnail { get; set; }
        public short Display { get; set; }
        public int CategoryId { get; set; }
        public List<int> Attributes { get; set; }
    }
}
