using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    public class AuditsController : Controller<Audit>
    {
        public AuditsController (
            IService<Audit> service
        ): base(
            service,
            "AuditId"
        ) { }
    }
}