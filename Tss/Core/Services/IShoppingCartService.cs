using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Services
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCartItemGet>> GetItems();
        Task<ShoppingCartItemAdd> AddItem(ShoppingCartAdd item);
        Task<ShoppingCartItemAdd> UpdateItem(int itemId, ShoppingCartUpdate item);
        Task<ShoppingCartItemAdd> RemoveItem(int itemId);
        Task<ShoppingCartItemAdd> SaveItemForLater(int itemId);
        Task<ShoppingCartItemAdd> MoveItemToCart(int itemId);
        Task<List<ShoppingCartItemGet>> GetSavedItems();
        Task<decimal> GetTotalAmount();
    }
}