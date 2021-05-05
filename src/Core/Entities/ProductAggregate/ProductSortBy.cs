using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public abstract class BaseProductSortBy : Enumeration
    {
        protected BaseProductSortBy(string message)
        : base(1, message)
        {
        }
    }

    public class ProductSortBy : BaseProductSortBy
    {
        public static ProductSortBy Name = new ProductSortBy("Name");
        public static ProductSortBy Price = new ProductSortBy("Price");
        public static ProductSortBy BestSelling = new ProductSortBy("BestSelling");
        public static ProductSortBy MostDiscounted = new ProductSortBy("MostDiscounted");
        public static ProductSortBy TopRated = new ProductSortBy("TopRated");
        public static ProductSortBy Newest = new ProductSortBy("Newest");

        public ProductSortBy(string message)
        : base(message)
        {
        }
    }
}
