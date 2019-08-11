using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    public class OrdersController : Controller<Orders>
    {
        public OrdersController (
            IService<Orders> service
        ): base(
            service,
            "OrderId"
        ) { }
    }
}