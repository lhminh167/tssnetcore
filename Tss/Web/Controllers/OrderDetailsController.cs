using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
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