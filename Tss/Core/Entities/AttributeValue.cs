using System;
using System.Collections.Generic;

namespace Tss.Core
{
    public partial class AttributeValue
    {
        public int AttributeValueId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }
    }
}
