using System.Collections.Generic;
using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeValueByNamesAndValuesSpecification : BaseSpecification<AttributeValue, AttributeValue>
    {
        public GetAttributeValueByNamesAndValuesSpecification(List<string> name, List<string> value, IAuthHelper authHelper)
        : base(
            av
            => (name.Count == 0 || name.Contains(av.Attribute.Name))
            && (value.Count == 0 || value.Contains(av.Value))
            ,
            av => av
        )
        {
            AddInclude(av => av.Attribute);
        }
    }

}
