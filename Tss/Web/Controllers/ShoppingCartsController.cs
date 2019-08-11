using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    public class ShoppingCartsController : Controller<ShoppingCart>
    {
        public ShoppingCartsController (
            IService<ShoppingCart> service
        ): base(
            service,
            "ShoppingCartId"
        ) { }
    }
}