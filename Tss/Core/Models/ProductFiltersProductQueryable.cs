using System.Linq;
using Sieve.Attributes;
using Tss.Core.Entities;

namespace Tss.Core.ProductFilters
{
    public class ProductFiltersProductQueryable
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal Price { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal DiscountedPrice { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Image { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Image2 { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Thumbnail { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public short Display { get; set; }

        public IQueryable<AttributeValue> Attributes { get; set; }        
    }

}
