using Ardalis.GuardClauses;

namespace Tss.FunctionalTests.Core.Models
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

        public ShoppingCartAuthInfo() { }

        public string CartId { get; set; }
        public string Token { get; set; }
    }
}
