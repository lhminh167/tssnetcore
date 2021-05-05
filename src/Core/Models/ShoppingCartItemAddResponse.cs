using System.Collections.Generic;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ShoppingCartItemAddResponse
    {
        public ShoppingCartItemAddResponse(ShoppingCart item, int totalQuantity, string loginToken)
        {
            Guard.Against.Null(item, nameof(Item));
            Guard.Against.NullOrWhiteSpace(loginToken, nameof(LoginToken));
            Guard.Against.OutOfRange(totalQuantity, nameof(TotalQuantity), 1, int.MaxValue);

            Item = item;
            LoginToken = loginToken;
            TotalQuantity = totalQuantity;
        }

        public ShoppingCart Item { get; private set; }           
        public int TotalQuantity { get; private set; }
        public string LoginToken { get; private set; }
    }
}
