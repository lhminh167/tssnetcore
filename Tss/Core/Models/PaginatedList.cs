using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class PaginatedList<T>
    {
        public PaginatedList(
            List<T> items,
            int count,
            int pageIndex,
            int pageSize,
            bool isPagingEnabled
        )
        {
            Items = items;

            if (!isPagingEnabled) return;
            Guard.Against.OutOfRange(pageSize, nameof(pageSize), 0, int.MaxValue);

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageIndex = pageIndex;
            Count = count;
            PageSize = pageSize;
            IsPagingEnabled = isPagingEnabled;
        }

        public IReadOnlyCollection<T> Items { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int Count { get; private set; }
        public bool HasPreviousPage => (PageIndex > 0);
        public bool HasNextPage => (PageIndex < TotalPages - 1);
    }
}