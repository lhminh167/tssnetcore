using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ShippingRegionController : Controller<ShippingRegion>
    {
        public ShippingRegionController (
            IService<ShippingRegion> service
        ): base(
            service,
            "ShippingRegionId"
        ) { }
    }
}