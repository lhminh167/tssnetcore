using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
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