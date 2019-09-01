using System.Threading.Tasks;
using System.Collections.Generic;
using Sieve.Models;

namespace Tss.Core.Generics
{
    public interface IService
    {
    }

    public interface IService<T> : IService where T : class
    {
        Task<List<T>> Get(SieveModel sieveModel);

        Task<T> Get(int id);
        
        Task<int> Put(T entity);
        
        Task<int> Post(T entity);

        Task<int> Delete(T entity);        
    }
}
