using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace Tss.Core.Entities
{
    public partial class AttributeValue
    {
        public AttributeValue(int attributeId, string value)
        {
            Guard.Against.OutOfRange(attributeId, nameof(attributeId), 1, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(value, nameof(value));

            AttributeId = attributeId;
            Value = value;
        }

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

        public void Update(AttributeValue attributeValue)
        {
            Guard.Against.Null(attributeValue, nameof(attributeValue));
            
            Value = attributeValue.Value;
        }

        public static IDictionary<string, string> Decode(string attributeList)
        {
            Guard.Against.Null(attributeList, nameof(attributeList));

            List<string> attributesAndAttributeValues = attributeList.Split(":").ToList();

            List<string> attributes = attributesAndAttributeValues.FirstOrDefault().Split("/").ToList();

            List<string> attributeValues = attributesAndAttributeValues.LastOrDefault().Split("/").ToList();

            IDictionary<string, string> dict = new Dictionary<string, string>();

            for (int i = 0; i < attributes.Count; i++)
            {
                dict.TryAdd(attributes[i], attributeValues[i]);
            }

            return dict;
        }

        public static string Encode(List<AttributeValue> attributeList)
        {            
            Guard.Against.Null(attributeList, nameof(attributeList));

            string attributes = string.Join("/", attributeList.Select(av => av.Attribute.Name));
            string attributeValues = string.Join("/", attributeList.Select(av => av.Value));
            return string.Join(":", attributes, attributeValues);
        }

        public void UpdateAttribute(int attributeId, string value)
        {
            Guard.Against.OutOfRange(attributeId, nameof(attributeId), 1, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(value, nameof(value));

            AttributeId = attributeId;
            Value = value;
        }
    }
}
