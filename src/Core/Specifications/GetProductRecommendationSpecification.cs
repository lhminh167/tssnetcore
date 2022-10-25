using System;
using System.Linq;
using Tss.Core.Entities;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetProductRecommendationSpecification : BaseSpecification<OrderDetail, Product>
    {
        public GetProductRecommendationSpecification(ProductRecommendationRequest input, IAuthHelper authHelper)
        : base(
            od
            => od.Order.OrderDetail.Any(od => od.ProductId == input.ProductId)
            && od.ProductId != input.ProductId
            && od.Order.CreatedOn > DateTime.Now.AddMonths(-1)
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

