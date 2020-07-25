using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Tss.Core.Interfaces;
using Tss.Infrastructure.Data;
using System.Collections.Generic;
using Tss.Core.Models;
using System.Linq.Expressions;

namespace Tss.Infrastructure.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        private readonly TssContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(
            TssContext context,
            IUnitOfWork unitOfWork
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(context));

            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWork.Register(this);
        }

        public virtual async Task<List<TEntity>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<PaginatedList<TOut>> GetPaginated<TOut>(
            ISpecification<TEntity, TOut> spec
        )
        {
            List<TOut> list = spec.IsPagingEnabled
                ? await ApplySpecification(spec).Skip(spec.PageIndex * spec.PageSize).Take(spec.PageSize).ToListAsync()
                : await ApplySpecification(spec).ToListAsync();

            return new PaginatedList<TOut>(
                list,
                await ApplySpecification(spec).CountAsync(),
                spec.PageIndex,
                spec.PageSize,
                spec.IsPagingEnabled
            );
        }

        public virtual async Task<List<TOut>> Get<TOut>(
            ISpecification<TEntity, TOut> spec
        )
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public virtual async Task<TOut> GetOne<TOut>(
            ISpecification<TEntity, TOut> spec
        )
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<int> Count(ISpecification<TEntity, TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public virtual async Task<TOut> GetMax<TOut>(
            Expression<Func<TEntity, TOut>> spec
        )
        {
            return await _dbSet.MaxAsync<TEntity, TOut>(spec);
        }

        public virtual async Task<TOut> GetMin<TOut>(
            Expression<Func<TEntity, TOut>> spec
        )
        {
            return await _dbSet.MinAsync<TEntity, TOut>(spec);
        }

        public virtual async Task<decimal> GetAverage(
            Expression<Func<TEntity, decimal>> spec
        )
        {
            return await _dbSet.AverageAsync(spec);
        }

        public virtual TEntity Put(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public virtual TEntity Post(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            return _dbSet.Remove(entity).Entity;
        }

        public virtual void DeleteRange(List<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public virtual void PutRange(List<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
        }

        public virtual void PostRange(List<TEntity> entity)
        {
            _dbSet.AddRange(entity);
        }

        private IQueryable<TOut> ApplySpecification<TOut>(ISpecification<TEntity, TOut> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbSet, spec);
        }

        public void Detach(object input)
        {
            _context.Entry(input).State = EntityState.Detached;
        }
    }
}
