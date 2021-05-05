using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeByIdSpecification : BaseSpecification<Attribute, Attribute>
    {
        public GetAttributeByIdSpecification(int attributeId, IAuthHelper authHelper)
        : base(
            a 
            => a.AttributeId == attributeId
            ,
            a => a
        )
        {
            SetIsNoTracking(true);
        }
    }

}
