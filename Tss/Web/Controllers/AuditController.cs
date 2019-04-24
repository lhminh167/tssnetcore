using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class AuditController : Controller<Audit>
    {
        public AuditController (
            IService<Audit> service
        ): base(
            service,
            "AuditId"
        ) { }
    }
}