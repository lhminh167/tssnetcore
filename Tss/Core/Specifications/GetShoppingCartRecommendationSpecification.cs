using System;
using System.Linq;
using Tss.Core.Entities;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetShoppingCartRecommendationSpecification : BaseSpecification<OrderDetail, Product>
    {
        public GetShoppingCartRecommendationSpecification(IAuthHelper authHelper)
        : base(
            od
            =>
            // promote products that were ordered together with 
            od.Order.OrderDetail.Any(od1 =>
                // the products in the shopping cart.
                od1.Product.ShoppingCart.Any(c => c.CartId == authHelper.GetShoppingCartId())
                &&
                od1.Product.ProductId != od.ProductId
            )
            ,
            od => od.Product
        )
        {
            // SetIsNoTracking(true);
            // AddInclude("Order.OrderDetail");
            // ApplyGroupBy(od => od.ProductId);
            // ApplyOrderBy(od => od.Product.OrderDetail.Count);
            // ApplyPaging(input.PageIndex, input.PageSize, input.IsPagingEnabled);
        }
    }
}

