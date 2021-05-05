using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeValueByNameSpecification : BaseSpecification<AttributeValue, AttributeValue>
    {
        public GetAttributeValueByNameSpecification(string name, IAuthHelper authHelper)
        : base(
            av
            => av.Attribute.Name == name
            ,
            a => a
        )
        {
            SetIsNoTracking(true);
            // AddInclude(av => av.Attribute);
        }
    }

}
