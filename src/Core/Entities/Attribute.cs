using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Extensions;
using Tss.Core.Models;

namespace Tss.Core.Entities
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

        public int AttributeId { get; private set; }
        public string Name { get; private set; }

        public virtual ICollection<AttributeValue> AttributeValue { get; private set; }

        public void Update(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Name = name;
        }
    }
}
