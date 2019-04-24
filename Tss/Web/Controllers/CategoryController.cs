using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class CategoryController : Controller<Category>
    {
        public CategoryController (
            IService<Category> service
        ): base(
            service,
            "CategoryId"
        ) { }
    }
}