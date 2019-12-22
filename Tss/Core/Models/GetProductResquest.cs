namespace Tss.Core.Models
{
    public class GetProductResquest
    {
        public string Keyword { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int CategoryId { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public object SortBy { get; set; }
        public int SortOrder { get; set; }
    }
}
