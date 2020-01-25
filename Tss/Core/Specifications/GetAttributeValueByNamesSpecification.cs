using System.Collections.Generic;
using System.Linq;
using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeValueByNamesSpecification : BaseSpecification<AttributeValue, AttributeValue>
    {
        public GetAttributeValueByNamesSpecification(List<string> name, IAuthHelper authHelper)
        : base(
            av
            => ProductAttribute.AllowedValues.Contains(av.Attribute.Name)
            ,
            av => av
        )
        {
            SetIsNoTracking(true);
            AddInclude(av => av.Attribute);
        }
    }

}
