using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    public class CategoriesController : Controller<Category>
    {
        public CategoriesController (
            IService<Category> service
        ): base(
            service,
            "CategoryId"
        ) { }
    }
}