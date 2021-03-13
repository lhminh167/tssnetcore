using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace Tss.FunctionalTests.Core.Models
{
    public partial class AttributeValue
    {
        public AttributeValue()
        {
            CustomerAttribute = new HashSet<CustomerAttribute>();
            ProductAttribute = new HashSet<ProductAttribute>();
        }

        public int AttributeValueId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }

        public virtual Attribute Attribute { get; set; }
        private ICollection<CustomerAttribute> CustomerAttribute { get; set; }
        private ICollection<ProductAttribute> ProductAttribute { get; set; }

        public string Name => Attribute?.Name;
    }
}
