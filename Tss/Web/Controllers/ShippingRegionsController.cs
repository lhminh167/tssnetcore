using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
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