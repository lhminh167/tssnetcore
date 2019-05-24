using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
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