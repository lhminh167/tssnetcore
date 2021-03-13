using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ReviewGetResponse : PaginatedList<ReviewResponse>
    {
        public ReviewGetResponse()
        {
        }

        public decimal AverageRating { get; set; }
    }
}