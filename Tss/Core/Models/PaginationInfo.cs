using System;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class PaginationInfo
    {
        public PaginationInfo(
            int count = 1, 
            int pageIndex = 1, 
            int pageSize = 1,
            bool isPagingEnabled = false
        )
        {
            Guard.Against.Zero(count, nameof(count));
            Guard.Against.Zero(pageSize, nameof(pageSize));
            Guard.Against.Zero(pageIndex, nameof(pageIndex));

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            Guard.Against.OutOfRange(pageIndex, nameof(pageIndex), 1, TotalPages);

            PageIndex = pageIndex;
            Count = count;
            PageSize = pageSize;
            IsPagingEnabled = isPagingEnabled;
        }
        public bool IsPagingEnabled { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int Count { get; private set; }
        public bool HasPreviousPage => (PageIndex > 1);
        public bool HasNextPage => (PageIndex < TotalPages);
    }
}