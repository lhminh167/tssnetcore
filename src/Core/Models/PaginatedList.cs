using System;
using System.Collections.Generic;
using System.Linq;
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

        public IReadOnlyList<T> Items { get; set; }
        public bool IsPagingEnabled { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }
        public bool HasPreviousPage => (PageIndex > 0);
        public bool HasNextPage => (PageIndex < TotalPages - 1);

        public PaginatedList<TOut> Select<TOut>(Func<T, TOut> converter)
        {
            return new PaginatedList<TOut>(
                Items.Select(converter).ToList(),
                Count,
                PageIndex,
                PageSize,
                IsPagingEnabled
            );
        }

    }
}