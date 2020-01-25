using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class PaginatedList<T> : PaginationInfo
    {
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        : base(count, pageIndex, pageSize, true)
        {
            Guard.Against.NullOrEmpty(items, nameof(items));

            Items = items;
        }
    
        public IReadOnlyCollection<T> Items { get; private set; }
    }
}