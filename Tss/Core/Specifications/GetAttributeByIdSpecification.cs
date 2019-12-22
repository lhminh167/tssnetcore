using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeByIdSpecification : BaseSpecification<Attribute, Attribute>
    {
        public GetAttributeByIdSpecification(int attributeId, IAuthHelper authHelper)
        : base(
            a 
            => (attributeId > 0 || a.AttributeId == attributeId)
            ,
            a => a
        )
        {
            AddInclude(av => av.AttributeValue);
        }
    }

}
