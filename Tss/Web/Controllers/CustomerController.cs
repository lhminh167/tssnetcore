using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class CustomerController : Controller<Customer>
    {
        public CustomerController (
            IService<Customer> service
        ): base(
            service,
            "CustomerId"
        ) { }
    }
}