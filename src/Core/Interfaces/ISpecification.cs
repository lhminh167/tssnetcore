using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Tss.Core.Interfaces
{
    public interface ISpecification<T, TOut>
    {
        Expression<Func<T, bool>> Criteria { get; }
        IReadOnlyList<Expression<Func<T, object>>> Includes { get; }
        IReadOnlyList<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
        Expression<Func<T, TOut>> Select { get; }

        bool IsNoTracking { get; }

        int PageIndex { get; }
        int PageSize { get; }
        bool IsPagingEnabled { get; }
    }
}
