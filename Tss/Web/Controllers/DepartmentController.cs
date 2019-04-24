using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class DepartmentController : Controller<Department>
    {
        public DepartmentController (
            IService<Department> service
        ): base(
            service,
            "DepartmentId"
        ) { }
    }
}