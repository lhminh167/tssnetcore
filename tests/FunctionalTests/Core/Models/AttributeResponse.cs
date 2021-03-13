using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Extensions;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class AttributeResponse
    {

        public AttributeResponse()
        {
            AttributeValue = new List<AttributeValueResponse>();
        }

        public int AttributeId { get; set; }
        public string Name { get; set; }

        public virtual List<AttributeValueResponse> AttributeValue { get; set; }
    }
}
