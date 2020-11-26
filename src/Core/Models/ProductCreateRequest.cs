using System.Collections.Generic;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductCreateRequest
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public double DiscountedPrice { get; private set; }
        public string Image { get; private set; }
        public string Image2 { get; private set; }
        public string Thumbnail { get; private set; }
        public short Display { get; private set; }
        public int CategoryId { get; private set; }
        public List<int> Attributes { get; private set; }
    }
}
