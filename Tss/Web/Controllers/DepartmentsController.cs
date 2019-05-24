using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class DepartmentsController : Controller<Department>
    {
        public DepartmentsController (
            IService<Department> service
        ): base(
            service,
            "DepartmentId"
        ) { }
    }
}