using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ReviewGetResponse : PaginatedList<ReviewResponse>
    {
        public ReviewGetResponse(
            PaginatedList<Review> list,
            decimal averageRating
        )
        : base(
            list.Items.Select(e => new ReviewResponse(e)).ToList(),
            list.Count,
            list.PageIndex,
            list.PageSize,
            list.IsPagingEnabled
        )
        {
            Guard.Against.BadRequest(averageRating < 0 || averageRating > 5, nameof(averageRating));

            AverageRating = averageRating;
        }

        public decimal AverageRating { get; set; }
    }
}