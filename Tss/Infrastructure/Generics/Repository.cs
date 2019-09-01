using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using Tss.Core.Generics;
using Tss.Infrastructure.Contexts;
using System.Collections.Generic;
using Sieve.Models;

namespace Tss.Infrastructure.Generics
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        
        private readonly TssContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SieveProcessor _sieveProcessor;

        public Repository(
            TssContext context,
            IUnitOfWork unitOfWork,
            SieveProcessor sieveProcessor
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(context));
            
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWork.Register(this);

            _sieveProcessor = sieveProcessor ?? throw new ArgumentNullException(nameof(sieveProcessor));
        }

        public virtual Task<List<TEntity>> Get(
            SieveModel sieve
        )
        {
            return _sieveProcessor.Apply<TEntity>(sieve, _dbSet).ToListAsync();
        }

        public virtual Task<TEntity> Get(int id)
        {
            return _dbSet.FindAsync(id);
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

    }
}
