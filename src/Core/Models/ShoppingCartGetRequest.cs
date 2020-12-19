namespace Tss.Core.Models
{
    public class ShoppingCartGetRequest : PaginationRequest
    {
        public ShoppingCartGetRequest(
            bool? buyNow
        )
        : base(
            0,
            0,
            false,
            null,
            true
        )
        {
            BuyNow = buyNow;
        }

        public ShoppingCartGetRequest(
            int pageIndex,
            int pageSize,
            bool isPagingEnabled,
            string sortBy,
            bool isDescending,
            bool buyNow
        )
        : base(pageIndex, pageSize, isPagingEnabled, sortBy, isDescending)
        {
            BuyNow = buyNow;
        }

        public bool? BuyNow { get; set; }
    }
}
