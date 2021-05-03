using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Models
{
    public class ProductGetRequest : PaginationRequest
    {
        public ProductGetRequest(
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
            double maxPrice,
            int display,
            bool includeDetails = false
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
            Display = display;
            IncludeDetails = includeDetails;
        }

        public string Keyword { get; set; }
        public List<string> Category { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public int Display { get; set; }
        public bool IncludeDetails { get; set; }
    }
}