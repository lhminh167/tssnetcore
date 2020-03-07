using Tss.Core.Entities;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetShoppingCartProductSpecification : BaseSpecification<Product, Product>
    {
        public GetShoppingCartProductSpecification(ShoppingCartItemAddRequest input)
        : base(
            p 
            => p.ProductId == input.ProductId
            ,
            p => p
        )
        {
            SetIsNoTracking(true);
            AddInclude("ProductAttribute.AttributeValue.Attribute");
        }
    }

}
