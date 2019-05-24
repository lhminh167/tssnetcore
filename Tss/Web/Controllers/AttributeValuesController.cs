using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class AttributeValuesController : Controller<AttributeValue>
    {
        public AttributeValuesController (
            IService<AttributeValue> service
        ): base(
            service,
            "AttributeValueId"
        ) { }
    }
}