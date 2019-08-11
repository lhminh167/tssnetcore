using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
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