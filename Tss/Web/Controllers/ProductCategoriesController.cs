using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
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