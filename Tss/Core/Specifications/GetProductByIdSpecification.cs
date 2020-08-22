using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetProductByIdSpecification : BaseSpecification<Product, Product>
    {
        public GetProductByIdSpecification(int productId, IAuthHelper authHelper)
        : base(
            p
            => p.ProductId == productId
            ,
            p => p
        )
        {
            SetIsNoTracking(false);
            AddInclude("ProductCategory.Category");
            AddInclude("ProductAttribute.AttributeValue.Attribute");
        }
    }
}
