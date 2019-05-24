using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
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