using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ShoppingCartAuthInfo
    {
        public ShoppingCartAuthInfo(string cartId, string token)
        {
            Guard.Against.NullOrWhiteSpace(cartId, nameof(cartId));
            Guard.Against.NullOrWhiteSpace(token, nameof(token));

            CartId = cartId;
            Token = token;
        }

        public string CartId { get; private set; }           
        public string Token { get; private set; }
    }
}
