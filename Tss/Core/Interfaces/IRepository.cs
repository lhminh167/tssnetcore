using System.Threading.Tasks;
using System.Collections.Generic;
using Sieve.Models;

namespace Tss.Core.Interfaces
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        Task<List<TEntity>> Get();

        Task<List<TEntity>> Get(SieveModel sieve);

        Task<List<TOut>> Get<TOut>(            
            ISpecification<TEntity, TOut> spec
        );

        Task<TOut> GetOne<TOut>(            
            ISpecification<TEntity, TOut> spec
        );

        Task<TEntity> Get(int id);
        
        TEntity Put(TEntity entity);
        
        TEntity Post(TEntity entity);

        TEntity Delete(TEntity entity);

        void PutRange(List<TEntity> entity);
        
        void PostRange(List<TEntity> entity);

        void DeleteRange(List<TEntity> entity);
    }
}
