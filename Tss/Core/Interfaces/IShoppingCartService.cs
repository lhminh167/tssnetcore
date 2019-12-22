using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCart>> GetItems();
        Task<ShoppingCartItemAddResponse> AddItem(ShoppingCartAddItemRequest item);
        Task<ShoppingCart> UpdateItem(int itemId, ShoppingCartUpdate item);
        Task<ShoppingCart> RemoveItem(int itemId);
        Task<ShoppingCart> SaveItemForLater(int itemId);
        Task<ShoppingCart> MoveItemToCart(int itemId);
        Task<List<ShoppingCart>> GetSavedItems();
        Task<double> GetTotalAmount();
    }
}