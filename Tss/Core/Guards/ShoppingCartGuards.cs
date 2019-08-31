using Tss.Core.Entities;
using Tss.Core.Exceptions;
using Tss.Core.Models;

namespace Ardalis.GuardClauses
{
    public static class ShoppingCartGuards
    {
        public static void ShoppingCartNullProduct(this IGuardClause guardClause, ShoppingCartAdd input, bool output)
        {
            if (!output)
                throw new ShoppingCartProductNotFoundException(input);
        }

        public static void ShoppingCartNullItem(this IGuardClause guardClause, int itemId, ShoppingCart output)
        {
            if (output == null)
                throw new ShoppingCartItemNotFoundException(itemId);
        }

        public static void ShoppingCartUnauthorizedAction(this IGuardClause guardClause, int inputCartId, int outputCartId)
        {
            if (inputCartId != outputCartId)
                throw new ShoppingCartUnauthorizedActionException();
        }
    }
}