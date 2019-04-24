using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class OrderDetailController : Controller<OrderDetail>
    {
        public OrderDetailController (
            IService<OrderDetail> service
        ): base(
            service,
            "OrderDetailId"
        ) { }
    }
}