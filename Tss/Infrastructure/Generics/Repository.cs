using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using Tss.Core.Generics;
using Tss.Infrastructure.Contexts;

namespace Tss.Infrastructure.Generics
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        
        private readonly TssContext _context;
        private readonly IUnitOfWork _unitOfWork;


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

        }

        public virtual IQueryable<TEntity> Get(
        )
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }

        public virtual Task<TEntity> Get(int id)
        {
            return _dbSet.FindAsync(id);
        }
        public virtual void Put(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

        public virtual void Post(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async void Delete(int id)
        {
            TEntity entity = await Get(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
    }
}
