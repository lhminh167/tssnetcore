using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class TaxController : Controller<Tax>
    {
        public TaxController (
            IService<Tax> service
        ): base(
            service,
            "TaxId"
        ) { }
    }
}