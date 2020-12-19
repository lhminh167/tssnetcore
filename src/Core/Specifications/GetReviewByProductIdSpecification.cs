using System;
using System.Linq.Expressions;
using Tss.Core.Entities;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Specifications
{
    public class GetReviewByProductIdSpecification : BaseSpecification<Review, Review>
    {
        public GetReviewByProductIdSpecification(ReviewGetRequest input, IAuthHelper authHelper)
        : base(
            r
            => r.ProductId == input.ProductId
            ,
            p => p
        )
        {
            SetIsNoTracking(true);
            AddInclude(r => r.Customer);
            ApplyPaging(input.PageIndex, input.PageSize, input.IsPagingEnabled);
            Sort(input);

        }

        private void Sort(ReviewGetRequest input)
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

        private Expression<Func<Review, object>> GetSortFunc(ReviewGetRequest input)
        => input.SortBy switch
        {
            _ =>
                p => p.CreatedOn,
        };

    }
}
