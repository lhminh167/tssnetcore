using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class PaginationRequest
    {
        public PaginationRequest(
            int pageIndex,
            int pageSize,
            bool isPagingEnabled,
            string sortBy,
            bool isDescending
        )
        {
            SortBy = sortBy;
            IsDescending = isDescending;

            if (!isPagingEnabled) return;
            Guard.Against.OutOfRange(pageSize, nameof(pageSize), 1, int.MaxValue);
            Guard.Against.OutOfRange(pageIndex, nameof(pageIndex), 0, int.MaxValue);

            PageIndex = pageIndex;
            PageSize = pageSize;
            IsPagingEnabled = isPagingEnabled;
        }

        public PaginationRequest(
        )
        {
            SortBy = null;
            IsDescending = true;
            PageIndex = 0;
            PageSize = 0;
            IsPagingEnabled = false;
        }

        public bool IsPagingEnabled { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public string SortBy { get; private set; }
        public bool IsDescending { get; private set; }
    }
}