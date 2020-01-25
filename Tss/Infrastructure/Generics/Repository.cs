using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Tss.Core.Interfaces;
using Tss.Infrastructure.Contexts;
using System.Collections.Generic;
using Tss.Core.Models;

namespace Tss.Infrastructure.Generics
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
            int count = await ApplySpecification(spec).CountAsync();
            List<TOut> items = await ApplySpecification(spec).ToListAsync();
            return new PaginatedList<TOut>(items, count, spec.PageIndex, spec.PageSize);
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
    }
}
