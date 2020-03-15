using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Models;
using System.Linq.Expressions;
using System;

namespace Tss.Core.Interfaces
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        Task<List<TEntity>> Get();
        Task<List<TOut>> Get<TOut>(ISpecification<TEntity, TOut> spec);
        Task<PaginatedList<TOut>> GetPaginated<TOut>(ISpecification<TEntity, TOut> spec);
        Task<int> Count(ISpecification<TEntity, TEntity> spec);
        Task<TOut> GetOne<TOut>(ISpecification<TEntity, TOut> spec);
        Task<TEntity> Get(int id);
        Task<TOut> GetMax<TOut>(Expression<Func<TEntity, TOut>> spec);
        Task<TOut> GetMin<TOut>(Expression<Func<TEntity, TOut>> spec);
        Task<decimal> GetAverage(Expression<Func<TEntity, decimal>> spec);
        TEntity Put(TEntity entity);
        TEntity Post(TEntity entity);
        TEntity Delete(TEntity entity);
        void PutRange(List<TEntity> entity);
        void PostRange(List<TEntity> entity);
        void DeleteRange(List<TEntity> entity);
        void Detach(object input);
    }
}
