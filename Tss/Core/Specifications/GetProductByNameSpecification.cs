using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetProductByNameSpecification : BaseSpecification<Product, Product>
    {
        public GetProductByNameSpecification(string name, IAuthHelper authHelper)
        : base(
            p 
            => p.Name == name
            ,
            p => p
        )
        {
            SetIsNoTracking(true);
        }
    }
}
