using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
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