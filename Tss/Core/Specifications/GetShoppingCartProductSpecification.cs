using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetShoppingCartProductSpecification : BaseSpecification<Product, Product>
    {
        public GetShoppingCartProductSpecification(ShoppingCartAddItemRequest input)
        : base(
            p 
            => p.ProductId == input.ProductId
            && p.CheckAttributes(input.Attributes)
            ,
            p => p
        )
        {
            AddInclude("ProductAttribute.AttributeValue.Attribute");
        }
    }

}
