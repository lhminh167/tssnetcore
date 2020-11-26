using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class AttributeUpdateRequest: Attribute
    {
        public new string Name { get; set; }
        public new int AttributeId { get; set; }
    }
}
