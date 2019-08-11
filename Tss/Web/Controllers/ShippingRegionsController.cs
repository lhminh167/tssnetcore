using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    public class ShippingRegionsController : Controller<ShippingRegion>
    {
        public ShippingRegionsController (
            IService<ShippingRegion> service
        ): base(
            service,
            "ShippingRegionId"
        ) { }
    }
}