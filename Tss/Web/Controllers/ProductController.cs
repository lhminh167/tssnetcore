using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ProductController : Controller<Product>
    {
        public ProductController (
            IService<Product> service
        ): base(
            service,
            "ProductId"
        ) { }
    }
}