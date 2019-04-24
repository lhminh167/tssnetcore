using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ShoppingCartController : Controller<ShoppingCart>
    {
        public ShoppingCartController (
            IService<ShoppingCart> service
        ): base(
            service,
            "ShoppingCartId"
        ) { }
    }
}