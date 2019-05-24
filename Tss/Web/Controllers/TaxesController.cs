using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class TaxesController : Controller<Tax>
    {
        public TaxesController (
            IService<Tax> service
        ): base(
            service,
            "TaxId"
        ) { }
    }
}