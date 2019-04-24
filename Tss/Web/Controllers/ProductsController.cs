using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ProductsController : Controller<Product>
    {
        public ProductsController (
            IService<Product> service
        ): base(
            service,
            "ProductId"
        ) { }
    }
}