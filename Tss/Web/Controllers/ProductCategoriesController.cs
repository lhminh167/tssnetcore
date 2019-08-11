using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    public class ProductCategoriesController : Controller<ProductCategory>
    {
        public ProductCategoriesController (
            IService<ProductCategory> service
        ): base(
            service,
            "ProductCategoryId"
        ) { }
    }
}