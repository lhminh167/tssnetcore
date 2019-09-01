using System.Collections.Generic;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductFilterAttribute
    {
        public int AttributeId { get; set; }
        public string Name { get; set; }
        public int AttributeValueId { get; set; }
        public string Value { get; set; }
    }
}
