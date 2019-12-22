using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetShoppingCartItemSpecification : BaseSpecification<ShoppingCart, ShoppingCart>
    {
        public GetShoppingCartItemSpecification(ShoppingCartAddItemRequest input, string cartId)
        : base(
            cart 
            => cart.CartId == cartId
            && cart.BuyNow
            && cart.Attributes == input.Attributes
            && cart.ProductId == input.ProductId
            ,
            cart => cart
        )
        {
            AddInclude("ProductAttribute.AttributeValue.Attribute");
        }
    }
}
