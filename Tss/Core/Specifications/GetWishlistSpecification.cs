using Tss.Core.Entities;

namespace Tss.Core.Specifications
{
    public class GetWishlistSpecification : BaseSpecification<ShoppingCart, ShoppingCart>
    {
        public GetWishlistSpecification(string cartId)
        : base(
            sc 
            => sc.CartId == cartId
            && !sc.BuyNow
            ,
            sc => sc
        )
        {
            SetIsNoTracking(true);
            AddInclude("Product.ProductAttribute.AttributeValue.Attribute");
        }
    }
}
