using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetProductSpecification : BaseSpecification<Product, Product>
    {
        public GetProductSpecification(IAuthHelper authHelper)
        : base(
            p 
            => true
            ,
            p => p
        )
        {
            SetIsNoTracking(true);
        }
    }
}
