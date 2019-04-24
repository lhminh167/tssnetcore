using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class AttributeController : Controller<Attribute>
    {
        public AttributeController (
            IService<Attribute> service
        ): base(
            service,
            "AttributeId"
        ) { }
    }
}