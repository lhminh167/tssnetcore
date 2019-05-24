using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
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