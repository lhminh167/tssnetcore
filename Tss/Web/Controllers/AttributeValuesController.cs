using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
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