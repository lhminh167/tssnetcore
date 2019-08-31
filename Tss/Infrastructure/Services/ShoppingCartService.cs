using Tss.Core.Entities;

using Sieve.Models;
using System;
using Sieve.Services;
using System.Linq;
using System.Collections.Generic;
using Tss.Core.Models;
using Tss.Core.Repositories;
using Tss.Core.Services;
using Tss.Core.Specifications;
using System.Threading.Tasks;
using Tss.Core.Generics;
using Microsoft.AspNetCore.Http;
using Tss.Core.Exceptions;
using Ardalis.GuardClauses;
using AutoMapper;

namespace Tss.Infrastructure.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartRepository _repo;
        private readonly IRepository<ShoppingCart> _genericRepo;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly string CartSessionKey = "CartId";
        public readonly int _cartId;

        public ShoppingCartService(
            IUnitOfWork unitOfWork,
            IShoppingCartRepository repo,
            IRepository<ShoppingCart> genericRepo,
            SieveProcessor sieveProcessor,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _genericRepo = genericRepo ?? throw new ArgumentNullException(nameof(genericRepo));
            _sieveProcessor = sieveProcessor ?? throw new ArgumentNullException(nameof(sieveProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _cartId = 1;
        }

        public int GetCartId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (string.IsNullOrEmpty(httpContext.Session.GetString(CartSessionKey)))
            {
                var tempCartId = Guid.NewGuid().ToString();
                httpContext.Session.SetString(CartSessionKey, tempCartId);
            }

            var carrIdString = httpContext.Session.GetString(CartSessionKey);
            
            int cartId;
            
            return int.TryParse(carrIdString, out cartId) ? cartId : 1;
        }

        public async Task<List<ShoppingCartItemGet>> GetItems()
        {
            return await _repo.GetItems(_cartId);
        }

        public async Task<ShoppingCartItemAdd> AddItem(ShoppingCartAdd input)
        {
            var isExisting = await _repo.ProductWithAttributesExists(input);
            
            Guard.Against.ShoppingCartNullProduct(input, isExisting);

            var dbItem = await _repo.AddGetItem(input, _cartId);
            
            if (dbItem == null)
            {
                dbItem = new ShoppingCart
                {
                    ProductId = input.ProductId,
                    CartId = _cartId,
                    Attributes = input.Attributes,
                    Quantity = input.Quantity,
                    BuyNow = true,
                    AddedOn = DateTime.Now
                };
    
                dbItem = _genericRepo.Post(dbItem);
            }
            else
            {
                dbItem.Quantity += input.Quantity;
            }

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ShoppingCartItemAdd>(dbItem);
        }

        public async Task<ShoppingCartItemAdd> UpdateItem(int itemId, ShoppingCartUpdate input)
        {
            var dbItem = await _genericRepo.Get(itemId);

            Guard.Against.ShoppingCartNullItem(itemId, dbItem);

            Guard.Against.ShoppingCartUnauthorizedAction(_cartId, dbItem.CartId);

            if (input.Quantity > 0) 
            {
                dbItem.Quantity = input.Quantity;
                dbItem.AddedOn = DateTime.Now;
                _genericRepo.Put(dbItem);
            }
            else
            {
                _genericRepo.Delete(dbItem);
            }

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ShoppingCartItemAdd>(dbItem);
        }

        public async Task<ShoppingCartItemAdd> RemoveItem(int itemId)
        {
            var dbItem = await _genericRepo.Get(itemId);

            Guard.Against.ShoppingCartNullItem(itemId, dbItem);

            Guard.Against.ShoppingCartUnauthorizedAction(_cartId, dbItem.CartId);

            _genericRepo.Delete(dbItem);
            
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ShoppingCartItemAdd>(dbItem);
        }

        public async Task<ShoppingCartItemAdd> SaveItemForLater(int itemId)
        {
            var dbItem = await _genericRepo.Get(itemId);

            Guard.Against.ShoppingCartNullItem(itemId, dbItem);

            Guard.Against.ShoppingCartUnauthorizedAction(_cartId, dbItem.CartId);

            dbItem.AddedOn = DateTime.Now;
            dbItem.BuyNow = false;
            
            _genericRepo.Put(dbItem);
            
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ShoppingCartItemAdd>(dbItem);
        }

        public async Task<ShoppingCartItemAdd> MoveItemToCart(int itemId)
        {
            var dbItem = await _genericRepo.Get(itemId);

            Guard.Against.ShoppingCartNullItem(itemId, dbItem);

            Guard.Against.ShoppingCartUnauthorizedAction(_cartId, dbItem.CartId);

            dbItem.AddedOn = DateTime.Now;
            dbItem.BuyNow = true;
            
            _genericRepo.Put(dbItem);
            
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ShoppingCartItemAdd>(dbItem);
        }

        public async Task<List<ShoppingCartItemGet>> GetSavedItems()
        {
            return await _repo.GetSavedItems(_cartId);
        }

        public async Task<decimal> GetTotalAmount()
        {
            return await _repo.GetTotalAmount(_cartId);
        }

    }
}