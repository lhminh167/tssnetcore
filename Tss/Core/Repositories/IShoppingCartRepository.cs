using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Generics;
using Tss.Core.Models;
using Tss.Core.Specifications;


namespace Tss.Core.Repositories
{
    public interface IShoppingCartRepository : IRepository
    {

        Task<bool> ProductWithAttributesExists(ShoppingCartAdd input);
        Task<ShoppingCart> AddGetItem(ShoppingCartAdd input, int cartId);
        Task<ShoppingCart> UpdateGetItem(int itemId);
        Task<List<ShoppingCartItemGet>> GetItems(int cartId);
        Task<List<ShoppingCartItemGet>> GetSavedItems(int cartId);
        Task<decimal> GetTotalAmount(int cartId);
    }
}
