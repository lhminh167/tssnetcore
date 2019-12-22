using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeValueByNameAndValueSpecification : BaseSpecification<AttributeValue, AttributeValue>
    {
        public GetAttributeValueByNameAndValueSpecification(string name, string value, IAuthHelper authHelper)
        : base(
            av 
            => (string.IsNullOrWhiteSpace(name) || av.Attribute.Name == name)
            && (string.IsNullOrWhiteSpace(value) || av.Value == value)
            ,
            av => av
        )
        {
            AddInclude(av => av.Attribute);
        }
    }

}
