using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ProductCategoryController : Controller<ProductCategory>
    {
        public ProductCategoryController (
            IService<ProductCategory> service
        ): base(
            service,
            "ProductCategoryId"
        ) { }
    }
}