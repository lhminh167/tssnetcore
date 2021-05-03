using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ShoppingCartGetRequestHelper
    {
        public ShoppingCartGetRequest Mock(
            bool buyNow
        )
        {
            return new ShoppingCartGetRequest(
                buyNow
            );
        }
    }
}
