using Tss.Models;
using Tss.Core;

namespace Tss.Controllers
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