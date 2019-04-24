using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ShippingController : Controller<Shipping>
    {
        public ShippingController (
            IService<Shipping> service
        ): base(
            service,
            "ShippingId"
        ) { }
    }
}