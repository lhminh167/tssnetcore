using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace Tss.FunctionalTests.Core.Models
{
    public class Attribute
    {
        public Attribute(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Name = name;
        }

        public Attribute()
        {
            AttributeValue = new HashSet<AttributeValue>();
        }

        public int AttributeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AttributeValue> AttributeValue { get; set; }

        public void Update(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Name = name;
        }
    }
}
