using Sieve.Attributes;
using Tss.Core.Entities;

namespace Tss.Core.ProductFilters
{
    public class ProductFiltersProductSpecifications
    {
        public string Keyword { get; }
        public int ColorId { get; }
        public int SizeId { get; }
        public int CategoryId { get; }
        public int MinPrice { get; }
        public int MaxPrice { get; }
        public int Page { get; }
        public int PageSize { get; }
        public object SortBy { get; }
        public int SortOrder { get; }
    }

}
