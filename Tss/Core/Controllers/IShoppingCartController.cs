using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Entities;

namespace Tss.Core.Controllers
{
    public interface IShoppingCartController
    {
        Task<IActionResult> AddItem(ShoppingCart product);

    }
}