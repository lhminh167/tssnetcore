using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    public class CustomersController : Controller<Customer>
    {
        public CustomersController (
            IService<Customer> service
        ): base(
            service,
            "CustomerId"
        ) { }
    }
}