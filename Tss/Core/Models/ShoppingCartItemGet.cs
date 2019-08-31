using Sieve.Attributes;

namespace Tss.Core.Models
{
    public class ShoppingCartItemGet
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int ItemId { get; set; }
                
        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Attributes { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public int Quantity { get; set; }
        
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

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
    }

}
