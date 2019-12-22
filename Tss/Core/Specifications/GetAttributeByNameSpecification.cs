using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetAttributeByNameSpecification : BaseSpecification<Attribute, Attribute>
    {
        public GetAttributeByNameSpecification(string name, IAuthHelper authHelper)
        : base(
            a
            => (string.IsNullOrWhiteSpace(name) || a.Name == name)
            ,
            a => a
        )
        {
            AddInclude(av => av.AttributeValue);
        }
    }

}
