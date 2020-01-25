using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class PaginationRequest
    {
        public PaginationRequest(
            int pageIndex = 1, 
            int pageSize = 1,
            bool isPagingEnabled = false
        )
        {
            Guard.Against.Zero(pageSize, nameof(pageSize));
            Guard.Against.Zero(pageIndex, nameof(pageIndex));

            PageIndex = pageIndex;
            PageSize = pageSize;
            IsPagingEnabled = isPagingEnabled;
        }
        public bool IsPagingEnabled { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
    }
}