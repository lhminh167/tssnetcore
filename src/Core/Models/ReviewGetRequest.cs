using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ReviewGetRequest : PaginationRequest
    {
        public ReviewGetRequest()
        : base(0, 8, true, null, true)
        {
        }


        public ReviewGetRequest(
            int productId
        )
        : base(0, 8, true, null, true)
        {
            ProductId = productId;
        }

        public ReviewGetRequest(
            int pageIndex,
            int pageSize,
            bool isPagingEnabled,
            string sortBy,
            bool isDescending,
            int productId
        )
        : base(pageIndex, pageSize, isPagingEnabled, sortBy, isDescending)
        {
            Guard.Against.OutOfRange(productId, nameof(productId), 1, int.MaxValue);

            ProductId = productId;
        }

        [Required, Range(0, int.MaxValue)]
        public int ProductId { get; set; }
    }
}