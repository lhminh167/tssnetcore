using System.Collections.Generic;
using System.Linq;
using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeValueByIdsSpecification : BaseSpecification<AttributeValue, AttributeValue>
    {
        public GetAttributeValueByIdsSpecification(
            List<int> attributeValueIds,
            IReadOnlyList<string> allowedValues,
            IAuthHelper authHelper
        )
        : base(
            av
            => attributeValueIds.Contains(av.AttributeValueId)
            && allowedValues.Contains(av.Attribute.Name)
            ,
            av => av
        )
        {
            SetIsNoTracking(true);
            AddInclude(av => av.Attribute);
        }
    }

}
