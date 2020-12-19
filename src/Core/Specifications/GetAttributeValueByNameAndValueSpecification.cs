using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeValueByNameAndValueSpecification : BaseSpecification<AttributeValue, AttributeValue>
    {
        public GetAttributeValueByNameAndValueSpecification(string name, string value, IAuthHelper authHelper)
        : base(
            av 
            => av.Attribute.Name == name
            && av.Value == value
            ,
            av => av
        )
        {
            SetIsNoTracking(true);
            AddInclude(av => av.Attribute);
        }
    }

}
