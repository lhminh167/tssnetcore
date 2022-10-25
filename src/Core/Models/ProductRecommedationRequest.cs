namespace Tss.Core.Models
{
    public class ProductRecommendationRequest : PaginationRequest
    {
        public ProductRecommendationRequest(
            int productId
        )
        : base(0, 8, true, null, true)
        {
            ProductId = productId;
        }

        public ProductRecommendationRequest(
            int pageIndex,
            int pageSize,
            bool isPagingEnabled,
            string sortBy,
            bool isDescending,
            int productId
        )
        : base(pageIndex, pageSize, isPagingEnabled, sortBy, isDescending)
        {
            ProductId = productId;
        }

        public int ProductId { get; private set; }
    }
}