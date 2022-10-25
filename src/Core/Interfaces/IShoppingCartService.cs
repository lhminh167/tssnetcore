using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartGetResponse> GetItems(ShoppingCartGetRequest input);
        Task<PaginatedList<Product>> GetRecommendations(PaginationRequest input);
        Task<ShoppingCartItemAddResponse> AddItem(ShoppingCartItemAddRequest input);
        Task<ShoppingCart> UpdateItem(int itemId, ShoppingCartItemUpdateRequest input);
        Task<ShoppingCart> RemoveItem(int itemId);
        Task<ShoppingCart> SaveItemForLater(int itemId);
        Task<ShoppingCart> MoveItemToCart(int itemId);
        Task<int> GetTotalQuantity(bool buyNow);
    }
}