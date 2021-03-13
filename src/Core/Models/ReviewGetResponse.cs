using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ReviewGetResponse : PaginatedList<Review>
    {
        public ReviewGetResponse(
            PaginatedList<Review> list,
            decimal averageRating
        )
        : base(
            list.Items.ToList(),
            list.Count,
            list.PageIndex,
            list.PageSize,
            list.IsPagingEnabled
        )
        {
            Guard.Against.Null(list, nameof(list));
            Guard.Against.BadRequest(averageRating < 0 || averageRating > 5, nameof(averageRating));

            AverageRating = averageRating;
        }

        public decimal AverageRating { get; private set; }
    }
}