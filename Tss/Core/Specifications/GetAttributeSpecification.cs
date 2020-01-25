using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeSpecification : BaseSpecification<Attribute, Attribute>
    {
        public GetAttributeSpecification(IAuthHelper authHelper)
        : base(
            a
            => true
            ,
            a => a
        )
        {
            SetIsNoTracking(true);
        }
    }

}
