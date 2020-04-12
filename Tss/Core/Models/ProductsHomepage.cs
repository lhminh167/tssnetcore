using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductsHomepage
    {
        public ProductsHomepage(
            PaginatedList<Product> topPanel,
            PaginatedList<Product> mostDiscounted,
            PaginatedList<Product> bestSelling,
            PaginatedList<Product> topRated,
            PaginatedList<Product> newest
        )
        {
            TopPanel = topPanel;
            MostDiscounted = mostDiscounted;
            BestSelling = bestSelling;
            TopRated = topRated;
            Newest = newest;
        }

        public PaginatedList<Product> TopPanel { get; private set; }
        public PaginatedList<Product> MostDiscounted { get; private set; }
        public PaginatedList<Product> BestSelling { get; private set; }
        public PaginatedList<Product> TopRated { get; private set; }
        public PaginatedList<Product> Newest { get; private set; }
    }
}