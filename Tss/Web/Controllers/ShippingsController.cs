using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
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