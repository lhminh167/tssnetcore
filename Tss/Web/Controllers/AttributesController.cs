using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
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