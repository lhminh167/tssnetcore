using System;
using System.Linq;
using System.Linq.Expressions;
using Tss.Core.Entities;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetProductSpecification : BaseSpecification<Product, Product>
    {
        public GetProductSpecification(
            IAuthHelper authHelper,
            ProductSearchRequest input
        )
        : base(
            p
            =>
            (
                string.IsNullOrWhiteSpace(input.Keyword)
                ||
                (
                    !string.IsNullOrWhiteSpace(input.Keyword)
                    &&
                    (
                        p.Name.ToUpper().Contains(input.Keyword.ToUpper())
                    || p.Description.ToUpper().Contains(input.Keyword.ToUpper())


                    // || p.ProductCategory
                    //     .Any(pc => pc.Category.Name.ToUpper().Contains(input.Keyword.ToUpper()))
                    // || p.ProductCategory
                    //     .Any(pc => pc.Category.Description.ToUpper().Contains(input.Keyword.ToUpper()))


                    // || p.ProductCategory
                    //     .Any(pc => pc.Category.Department.Name.Contains(input.Keyword.ToUpper()))
                    // || p.ProductCategory
                    //     .Any(pc => pc.Category.Department.Description.Contains(input.Keyword.ToUpper()))
                    )
                )
            )
            &&
            (
                string.IsNullOrWhiteSpace(input.Color)
                ||
                (
                    !string.IsNullOrWhiteSpace(input.Color)
                    &&

                        p.ProductAttribute.Any(x =>
                            x.AttributeValue.Value.ToUpper() == input.Color.ToUpper()
                            && x.AttributeValue.Attribute.Name.ToUpper() == "Color".ToUpper()
                        )

                )
            )
            &&
            (
                string.IsNullOrWhiteSpace(input.Size)
                ||
                (
                    !string.IsNullOrWhiteSpace(input.Size)
                    &&

                        p.ProductAttribute.Any(x =>
                            x.AttributeValue.Value.ToUpper() == input.Size.ToUpper()
                            && x.AttributeValue.Attribute.Name.ToUpper() == "Size".ToUpper()
                        )

                )
            )
            &&
            (
                input.Category.Count == 0
                ||
                (
                        input.Category.Count > 0
                        &&
                        p.ProductCategory.Any(pc => input.Category
                            .Select(c => c.ToUpper())
                            .Contains(pc.Category.Name.ToUpper())
                        )
                )
            )
            &&
            (
                input.MinPrice == 0.0
                ||
                (
                    input.MinPrice > 0.0
                    &&
                    (
                        (p.DiscountedPrice > 0.0 && p.DiscountedPrice >= input.MinPrice)
                        ||
                        (p.DiscountedPrice == 0.0 && p.Price >= input.MinPrice)
                    )
                )
            )
            &&
            (
                input.MaxPrice == 0.0
                ||
                (
                    input.MaxPrice > 0.0
                    &&
                    (
                        (p.DiscountedPrice > 0.0 && p.DiscountedPrice <= input.MaxPrice)
                        ||
                        (p.DiscountedPrice == 0.0 && p.Price <= input.MaxPrice)
                    )
                )
            )
            ,
            p => p
        )
        {
            SetIsNoTracking(true);
            ApplyPaging(input.PageIndex, input.PageSize, input.IsPagingEnabled);
            Sort(input);
        }

        private void Sort(ProductSearchRequest input)
        {
            if (input.IsDescending)
            {
                ApplyOrderByDescending(GetSortFunc(input));
            }
            else
            {
                ApplyOrderBy(GetSortFunc(input));
            }
        }

        private Expression<Func<Product, object>> GetSortFunc(ProductSearchRequest input)
        => input.SortBy switch
        {
            "Name" =>
                p => p.Name,
            "Price" =>
                p => p.DiscountedPrice > 0 ? p.DiscountedPrice : p.Price,
            "BestSelling" =>
                p => p.OrderDetail.Count,
            "MostDiscounted" =>
                p => p.DiscountedPrice / p.Price * 100.0,
            "TopRated" =>
                p => (decimal)p.Review.Average(r => (decimal)r.Rating),
            "Newest" =>
                p => p.ProductId,
            _ =>
                p => p.ProductId,
        };

    }
}
