using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ProductsHomepage
    {
        public ProductsHomepage() { }

        public PaginatedList<Product> TopPanel { get; set; }
        public PaginatedList<Product> MostDiscounted { get; set; }
        public PaginatedList<Product> BestSelling { get; set; }
        public PaginatedList<Product> TopRated { get; set; }
        public PaginatedList<Product> Newest { get; set; }
    }
}