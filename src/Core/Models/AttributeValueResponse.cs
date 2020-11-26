using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class AttributeValueResponse
    {
        public AttributeValueResponse(AttributeValue attributeValue)
        {
            Guard.Against.Null(attributeValue, nameof(AttributeValue));

            AttributeValueId = attributeValue.AttributeValueId;
            AttributeId = attributeValue.AttributeId;
            Value = attributeValue.Value;
            Name = attributeValue?.Attribute?.Name;
        }

        private AttributeValueResponse()
        {
        }

        public int AttributeValueId { get; private set; }
        public int AttributeId { get; private set; }
        public string Value { get; private set; }
        public string Name { get; private set; }
    }
}
