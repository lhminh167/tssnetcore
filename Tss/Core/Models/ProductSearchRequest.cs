using System.Collections.Generic;

namespace Tss.Core.Models
{
    public class ProductSearchRequest : PaginationRequest
    {
        public ProductSearchRequest(
            int pageIndex,
            int pageSize,
            bool isPagingEnabled,
            string sortBy,
            bool isDescending,
            string keyword,
            string color,
            string size,
            List<string> category,
            double minPrice,
            double maxPrice
        )
        : base(pageIndex, pageSize, isPagingEnabled, sortBy, isDescending)
        {
            Keyword = keyword;
            Color = color;
            Size = size;
            Category = category.TrueForAll(c => string.IsNullOrWhiteSpace(c)) || category == null
                ? new List<string>()
                : category;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public string Keyword { get; private set; }
        public List<string> Category { get; private set; }
        public string Color { get; private set; }
        public string Size { get; private set; }
        public double MinPrice { get; private set; }
        public double MaxPrice { get; private set; }
    }
}