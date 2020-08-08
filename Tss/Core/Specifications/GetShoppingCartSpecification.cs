using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetShoppingCartSpecification : BaseSpecification<ShoppingCart, ShoppingCart>
    {
        public GetShoppingCartSpecification(ShoppingCartGetRequest input, string cartId)
        : base(
            cart
            => cart.CartId == cartId
            && (
                !input.BuyNow.HasValue
                || cart.BuyNow == input.BuyNow
            )
            ,
            cart => cart
        )
        {
            SetIsNoTracking(true);
            AddInclude(c => c.Product);
            ApplyPaging(input.PageIndex, input.PageSize, input.IsPagingEnabled);
        }
    }
}
