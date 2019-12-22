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
        [Key]
        public int AttributeValueId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
    }
}
