using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Extensions;
using Tss.Core.Models;

namespace Tss.Core.Models
{
    public class AttributeResponse
    {

        public AttributeResponse()
        {
            AttributeValue = new HashSet<AttributeValueResponse>();
        }

        public int AttributeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AttributeValueResponse> AttributeValue { get; set; }
    }
}
