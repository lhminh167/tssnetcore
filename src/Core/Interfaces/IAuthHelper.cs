using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IAuthHelper
    {
        string GetLoginToken(Customer customer);
        ShoppingCartAuthInfo GetShoppingCartAuthInfo();
        string GetRole();
        string GetShoppingCartId();
        int GetCustomerId();
    }
}