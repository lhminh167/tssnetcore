using Tss.Core.Entities;
using System;
using System.Collections.Generic;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Tss.Core.Specifications;
using System.Linq;
using Tss.Core.Models;

namespace Tss.Core.Services
{
    public partial class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ShoppingCart> _shoppingCartRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<OrderDetail> _orderDetailRepo;
        private readonly IAuthHelper _authHelper;

        public ShoppingCartService(
            IUnitOfWork unitOfWork,
            IRepository<ShoppingCart> shoppingCartRepo,
            IRepository<Product> productRepo,
            IRepository<OrderDetail> orderDetailRepo,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _shoppingCartRepo = shoppingCartRepo ?? throw new ArgumentNullException(nameof(shoppingCartRepo));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _orderDetailRepo = orderDetailRepo ?? throw new ArgumentNullException(nameof(orderDetailRepo));
            _authHelper = authHelper ?? throw new ArgumentNullException(nameof(authHelper));
        }

        public async Task<ShoppingCartGetResponse> GetItems(ShoppingCartGetRequest input)
        {
            return new ShoppingCartGetResponse(
                await _shoppingCartRepo.GetPaginated(new GetShoppingCartSpecification(input, _authHelper.GetShoppingCartId())),
                await GetTotalCost(input.BuyNow.Value),
                await GetTotalQuantity(input.BuyNow.Value)
            );
        }

        public async Task<PaginatedList<Product>> GetRecommendations(
            PaginationRequest input
        )
        {
            List<Product> list = await _orderDetailRepo.Get(new GetShoppingCartRecommendationSpecification(
                _authHelper
            ));

            list = list.GroupBy(e => e.ProductId).OrderByDescending(p => p.Count()).SelectMany(p => p).Distinct().ToList();

            List<Product> paginatedList = list.Skip(input.PageIndex * input.PageSize).Take(input.PageSize).ToList();

            return new PaginatedList<Product>(
                input.IsPagingEnabled ? paginatedList : list,
                list.Count(),
                input.PageIndex,
                input.PageSize,
                input.IsPagingEnabled
            );
        }

        public async Task<ShoppingCartItemAddResponse> AddItem(ShoppingCartItemAddRequest input)
        {
            Product product = await _productRepo.GetOne(new GetShoppingCartProductSpecification(input));
            Guard.Against.NotFound(product, nameof(Product));
            product.CheckAttributes(input.Attributes);

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

            int totalQuantity = await GetTotalQuantityByShoppingCartId(input.BuyNow.Value, authInfo.CartId);

            return new ShoppingCartItemAddResponse(item, totalQuantity, authInfo.Token);
        }

        public async Task<ShoppingCart> UpdateItem(int itemId, ShoppingCartItemUpdateRequest input)
        {
            var item = await _shoppingCartRepo.GetOne(new GetShoppingCartItemSpecification(itemId, _authHelper));
            Guard.Against.NotFound(item, "Item");

            if (input.Quantity > 0)
            {
                item.Update(input);
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
            var item = await _shoppingCartRepo.GetOne(new GetShoppingCartItemSpecification(itemId, _authHelper));
            Guard.Against.NotFound(item, "Item");

            _shoppingCartRepo.Delete(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<ShoppingCart> SaveItemForLater(int itemId)
        {
            return await ChangeBuyNow(itemId);
        }

        public async Task<ShoppingCart> MoveItemToCart(int itemId)
        {
            return await ChangeBuyNow(itemId);
        }

        private async Task<ShoppingCart> ChangeBuyNow(int itemId)
        {
            var item = await _shoppingCartRepo.GetOne(new GetShoppingCartItemSpecification(itemId, _authHelper));
            Guard.Against.NotFound(item, "Item");

            ShoppingCart sameItem = await _shoppingCartRepo.GetOne(new GetShoppingCartItemSpecification(
                new ShoppingCartItemAddRequest(
                    item.ProductId,
                    item.Quantity,
                    item.Attributes,
                    !item.BuyNow
                ),
                _authHelper.GetShoppingCartId()
            ));

            if (sameItem == null)
            {
                item.ChangeBuyNow();
                _shoppingCartRepo.Put(item);
            }
            else
            {
                sameItem.AddUpQuantity(item.Quantity);
                _shoppingCartRepo.Put(sameItem);
                _shoppingCartRepo.Delete(item);
                item = sameItem;
            }

            await _unitOfWork.SaveAsync();
            return item;
        }


        public async Task<double> GetTotalCost(bool buyNow)
        {
            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(new ShoppingCartGetRequest(buyNow), _authHelper.GetShoppingCartId()));

            return items.Sum(item =>
                (
                    item.Product.DiscountedPrice.Equals(null) ?
                    item.Product.DiscountedPrice :
                    item.Product.Price
                )
                * item.Quantity
            );
        }

        public async Task<int> GetTotalQuantityByShoppingCartId(bool buyNow, string shoppingCartId)
        {
            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(new ShoppingCartGetRequest(buyNow), shoppingCartId));

            return items.Sum(item => item.Quantity);
        }

        public async Task<int> GetTotalQuantity(bool buyNow)
        {
            return await GetTotalQuantityByShoppingCartId(buyNow, _authHelper.GetShoppingCartId());
        }

        public async Task RemoveCart()
        {
            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(new ShoppingCartGetRequest(null), _authHelper.GetShoppingCartId()));
            Guard.Against.NotFound(items, "Shopping Cart");

            _shoppingCartRepo.DeleteRange(items);
            await _unitOfWork.SaveAsync();
        }
    }
}