using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class CustomerAttributeResponse
    {
        private CustomerAttributeResponse()
        {
        }

        public int CustomerId { get; set; }
        public int AttributeValueId { get; set; }

        public virtual AttributeValueResponse AttributeValue { get; set; }
        public virtual CustomerResponse Customer { get; set; }

        public static IReadOnlyList<string> AllowedValues => new List<string> { "Role" };
    }
}
