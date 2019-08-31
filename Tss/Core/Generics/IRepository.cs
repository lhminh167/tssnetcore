using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Sieve.Models;

namespace Tss.Core.Generics
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : class
    {
        Task<List<T>> Get(SieveModel sieve);

        Task<T> Get(int id);
        
        T Put(T entity);
        
        T Post(T entity);

        T Delete(T entity);

        void PutRange(List<T> entity);
        
        void PostRange(List<T> entity);

        void DeleteRange(List<T> entity);
    }
}
