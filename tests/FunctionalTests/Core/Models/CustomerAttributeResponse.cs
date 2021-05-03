using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace Tss.FunctionalTests.Core.Models
{
    public class CustomerAttributeResponse
    {
        public CustomerAttributeResponse()
        {
        }

        public int CustomerId { get; set; }
        public int AttributeValueId { get; set; }

        public virtual AttributeValueResponse AttributeValue { get; set; }
        public virtual CustomerResponse Customer { get; set; }

        public static List<string> AllowedAttributeNames => new List<string> { "Role" };
    }
}
