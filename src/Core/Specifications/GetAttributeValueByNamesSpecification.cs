using System.Collections.Generic;
using System.Linq;
using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeValueByNamesSpecification : BaseSpecification<AttributeValue, AttributeValue>
    {
        public GetAttributeValueByNamesSpecification(
            List<string> name,
            IReadOnlyList<string> allowedValues,
            IAuthHelper authHelper
        )
        : base(
            av
            => allowedValues.Contains(av.Attribute.Name)
            ,
            av => av
        )
        {
            SetIsNoTracking(true);
            AddInclude(av => av.Attribute);
        }
    }

}
