using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Tss.Infrastructure.Contexts;
using Tss.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Generics;
using Microsoft.AspNetCore.Http;
using Tss.Core.Repositories;
using Tss.Core.Models;

namespace Tss.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly TssContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartRepository(
            TssContext context,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWork.Register(this);
        }

        public async Task<bool> ProductWithAttributesExists(ShoppingCartAdd input)
        {
            var attributes = input.DecodeAttributes();

            var list = await (
                from p in _context.Product
                join pa in _context.ProductAttribute
                on p.ProductId equals pa.ProductId
                join av in _context.AttributeValue
                on pa.AttributeValueId equals av.AttributeValueId

                where p.ProductId == input.ProductId
                && attributes.Any(a => a == av.Value)

                select av.Value
            ).ToListAsync();

            return attributes.Except(list).Count().Equals(0);

        }

        public async Task<ShoppingCart> AddGetItem(ShoppingCartAdd input, int cartId)
        {

            return await (
                from sc in _context.ShoppingCart
                where sc.CartId == cartId &&
                sc.ProductId == input.ProductId &&
                sc.Attributes == input.Attributes
                select sc
            ).SingleOrDefaultAsync();
        }

        public async Task<ShoppingCart> UpdateGetItem(int itemId)
        {

            return await (
                from sc in _context.ShoppingCart
                where sc.ItemId == itemId
                select sc
            ).SingleOrDefaultAsync();
        }

        public async Task<List<ShoppingCartItemGet>> GetItems(int cartId)
        {
            return await (
                from sc in _context.ShoppingCart
                join p in  _context.Product
                on sc.ProductId equals p.ProductId
                where sc.CartId == cartId
                && sc.BuyNow.Value
                select new ShoppingCartItemGet {
                    ItemId = sc.ItemId,
                    ProductId = sc.ProductId,
                    Attributes = sc.Attributes,
                    Quantity = sc.Quantity,
                    Name = p.Name,
                    Price = p.Price,
                    DiscountedPrice = p.DiscountedPrice,
                    Image = p.Image,
                    Image2 = p.Image2,
                    Thumbnail = p.Thumbnail
                }
            ).ToListAsync();
        }

        public async Task<List<ShoppingCartItemGet>> GetSavedItems(int cartId)
        {
            return await (
                from sc in _context.ShoppingCart
                join p in  _context.Product
                on sc.ProductId equals p.ProductId
                where sc.CartId == cartId
                && !sc.BuyNow.Value
                select new ShoppingCartItemGet {
                    ItemId = sc.ItemId,
                    ProductId = sc.ProductId,
                    Attributes = sc.Attributes,
                    Quantity = sc.Quantity,
                    Name = p.Name,
                    Price = p.Price,
                    DiscountedPrice = p.DiscountedPrice,
                    Image = p.Image,
                    Image2 = p.Image2,
                    Thumbnail = p.Thumbnail
                }
            ).ToListAsync();
        }

        public async Task<decimal> GetTotalAmount(int cartId)
        {
            return await (
                from sc in _context.ShoppingCart
                join p in  _context.Product
                on sc.ProductId equals p.ProductId
                where sc.CartId == cartId
                && sc.BuyNow.Value
                select new ShoppingCartItemGet {
                    Quantity = sc.Quantity,
                    Price = p.Price,
                    DiscountedPrice = p.DiscountedPrice
                }
            ).SumAsync(x =>
                (
                    x.DiscountedPrice.Equals(null) ?
                    x.DiscountedPrice :
                    x.Price
                ) 
                * x.Quantity
            );
        }
    }
}