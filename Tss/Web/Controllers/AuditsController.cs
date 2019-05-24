using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
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