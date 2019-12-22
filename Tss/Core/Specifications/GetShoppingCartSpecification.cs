using Tss.Core.Entities;

namespace Tss.Core.Specifications
{
    public class GetShoppingCartSpecification : BaseSpecification<ShoppingCart, ShoppingCart>
    {
        public GetShoppingCartSpecification(string cartId)
        : base(
            cart 
            => cart.CartId == cartId 
            && cart.BuyNow
            ,
            cart => cart
        )
        {
            AddInclude("ProductAttribute.AttributeValue.Attribute");
        }
    }
}
