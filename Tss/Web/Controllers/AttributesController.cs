using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class AttributesController : Controller<Attribute>
    {
        public AttributesController (
            IService<Attribute> service
        ): base(
            service,
            "AttributeId"
        ) { }
    }
}