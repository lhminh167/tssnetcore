using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Entities
{
    public class Product : IAggregateRoot
    {
        public Product()
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

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
