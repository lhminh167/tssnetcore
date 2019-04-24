using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class AttributeValueController : Controller<AttributeValue>
    {
        public AttributeValueController (
            IService<AttributeValue> service
        ): base(
            service,
            "AttributeValueId"
        ) { }
    }
}