using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public abstract class BaseSpecification<T, TOut> : ISpecification<T, TOut>
    {
        protected BaseSpecification(
            Expression<Func<T, bool>> criteria,
            Expression<Func<T, TOut>> select
        )
        {
            Criteria = criteria;
            Select = select;
            _includes = new List<Expression<Func<T, object>>>();
            _includeStrings = new List<string>();
            IsPagingEnabled = false;
        }

        public Expression<Func<T, bool>> Criteria { get; private set; }
        public Expression<Func<T, TOut>> Select { get; private set; }
        public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes;
        public IReadOnlyList<string> IncludeStrings => _includeStrings;
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }

        public bool IsNoTracking { get; private set; }

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public bool IsPagingEnabled { get; private set; }


        private List<Expression<Func<T, object>>> _includes;
        private List<string> _includeStrings;

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            _includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            _includeStrings.Add(includeString);
        }

        protected virtual void SetIsNoTracking(bool isNoTracking)
        {
            IsNoTracking = isNoTracking;
        }

        protected virtual void ApplyPaging(int pageIndex, int pageSize, bool isPagingEnabled)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsPagingEnabled = isPagingEnabled;
        }

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}
