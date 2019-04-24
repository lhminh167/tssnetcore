using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
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