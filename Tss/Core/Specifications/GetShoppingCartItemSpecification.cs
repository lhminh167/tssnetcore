using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetShoppingCartItemSpecification : BaseSpecification<ShoppingCart, ShoppingCart>
    {
        public GetShoppingCartItemSpecification(ShoppingCartItemAddRequest input, string cartId)
        : base(
            cart
            => cart.CartId == cartId
            && cart.BuyNow == input.BuyNow
            && cart.Attributes == input.Attributes
            && cart.ProductId == input.ProductId
            ,
            cart => cart
        )
        {
            SetIsNoTracking(true);
        }
    }
}
