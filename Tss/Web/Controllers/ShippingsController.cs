using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ShippingsController : Controller<Shipping>
    {
        public ShippingsController (
            IService<Shipping> service
        ): base(
            service,
            "ShippingId"
        ) { }
    }
}