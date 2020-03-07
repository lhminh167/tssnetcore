using System.Linq;
using Tss.Core.Entities;

namespace Tss.Core.Specifications
{
    public class ProductDetailGetRecommendationSpecification : BaseSpecification<OrderDetail, Product>
    {
        public ProductDetailGetRecommendationSpecification(int productId)
        : base(
            od
            => od.ProductId == productId
            && od.Product.OrderDetail.Any(od2 => od2.Product.ProductId != productId)
            ,
            od => od.Product
        )
        {
            SetIsNoTracking(true);
            AddInclude("Product.OrderDetail");
            ApplyGroupBy(od => od.Product.ProductId);
            ApplyOrderBy(od => od.Product.OrderDetail.Count);
        }
    }
}

