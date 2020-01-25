using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tss.Core.Interfaces;

namespace Tss.Infrastructure.Generics
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<TOut> GetQuery<TOut>(IQueryable<T> inputQuery, ISpecification<T, TOut> specification)
        {
            IQueryable<T> query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Includes all expression-based includes
            if (specification.Includes != null)
            {
                query = specification.Includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            // Include any string-based include statements
            if (specification.IncludeStrings != null)
            {
                query = specification.IncludeStrings.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            if (specification.IsNoTracking)
                query = query.AsNoTracking();

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query
                    .Skip((specification.PageIndex - 1) * specification.PageSize)
                    .Take(specification.PageSize);
            }

            if (typeof(TOut) == typeof(T))
            {
                return (IQueryable<TOut>)query;
            }
            
            return query.Select(specification.Select);
        }
    }
}
