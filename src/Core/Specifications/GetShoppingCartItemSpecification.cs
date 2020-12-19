using Tss.Core.Entities;
using Tss.Core.Interfaces;
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

        public GetShoppingCartItemSpecification(int itemId, IAuthHelper authHelper)
        : base(
            entry
            => entry.ItemId == itemId
            && (
                entry.CartId == authHelper.GetShoppingCartId()
                || authHelper.GetRole() == Role.Admin.Message
            ),
            entry => entry
        )
        {
            SetIsNoTracking(true);
        }

    }
}
