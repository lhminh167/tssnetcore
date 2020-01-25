using Tss.Core.Entities;
using System;
using System.Collections.Generic;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Tss.Core.Specifications;
using System.Linq;
using Tss.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tss.Core.Services
{
    public partial class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ShoppingCart> _shoppingCartRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IAuthHelper _authHelper;

        public ShoppingCartService(
            IUnitOfWork unitOfWork,
            IRepository<ShoppingCart> shoppingCartRepo,
            IRepository<Product> productRepo,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _shoppingCartRepo = shoppingCartRepo ?? throw new ArgumentNullException(nameof(shoppingCartRepo));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _authHelper = authHelper ?? throw new ArgumentNullException(nameof(authHelper));
        }

        public async Task<List<ShoppingCart>> GetItems()
        {
            return await _shoppingCartRepo.Get(new GetShoppingCartSpecification(_authHelper.GetShoppingCartId()));
        }

        public async Task<ShoppingCartItemAddResponse> AddItem(ShoppingCartItemAddRequest input)
        {
            Product product = await _productRepo.GetOne(new GetShoppingCartProductSpecification(input));  
            if (!product.CheckAttributes(input.Attributes))
                Guard.Against.NotFound(null, "product with such attributes was");

            ShoppingCartAuthInfo authInfo = _authHelper.GetShoppingCartAuthInfo();
            ShoppingCart item = await _shoppingCartRepo.GetOne(new GetShoppingCartItemSpecification(input, authInfo.CartId));
            if (item == null)
            {
                item = new ShoppingCart(input, authInfo.CartId);
                _shoppingCartRepo.Post(item);
            }
            else
            {
                item.AddUpQuantity(input.Quantity);
                _shoppingCartRepo.Put(item);
            }
            await _unitOfWork.SaveAsync();
            
            int totalQuantity = await GetTotalQuantity();

            return new ShoppingCartItemAddResponse(item, totalQuantity, authInfo.Token);
        }

        public async Task<ShoppingCart> UpdateItem(int itemId, ShoppingCartItemUpdateRequest input)
        {
            var item = await _shoppingCartRepo.Get(itemId);
            Guard.Against.NotFound(item, "Item");

            if (input.Quantity > 0)
            {
                item.UpdateItem(input);
                _shoppingCartRepo.Put(item);
            }
            else
            {
                _shoppingCartRepo.Delete(item);
            }

            await _unitOfWork.SaveAsync();

            return item;
        }

        public async Task<ShoppingCart> RemoveItem(int itemId)
        {
            ShoppingCart item = await _shoppingCartRepo.Get(itemId);
            Guard.Against.NotFound(item, "Item");

            _shoppingCartRepo.Delete(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<ShoppingCart> SaveItemForLater(int itemId)
        {
            ShoppingCart item = await _shoppingCartRepo.Get(itemId);
            Guard.Against.NotFound(item, "Item");

            item.SaveItemForLater();
            _shoppingCartRepo.Put(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<ShoppingCart> MoveItemToCart(int itemId)
        {
            ShoppingCart item = await _shoppingCartRepo.Get(itemId);
            Guard.Against.NotFound(item, "Item");

            item.MoveItemToCart();
            _shoppingCartRepo.Put(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<List<ShoppingCart>> GetSavedItems()
        {
            return await _shoppingCartRepo.Get(new GetWishlistSpecification(_authHelper.GetShoppingCartId()));
        }

        public async Task<double> GetTotalCost()
        {
            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(_authHelper.GetShoppingCartId()));

            return items.Sum(item =>
                (
                    item.Product.DiscountedPrice.Equals(null) ?
                    item.Product.DiscountedPrice :
                    item.Product.Price
                )
                * item.Quantity
            );
        }

        public async Task<int> GetTotalQuantity()
        {
            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(_authHelper.GetShoppingCartId()));

            return items.Sum(item => item.Quantity);
        }

        public async Task RemoveCart()
        {
            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(_authHelper.GetShoppingCartId()));
            Guard.Against.NotFound(items, "Shopping Cart");

            _shoppingCartRepo.DeleteRange(items);
            await _unitOfWork.SaveAsync();
        }
    }
}