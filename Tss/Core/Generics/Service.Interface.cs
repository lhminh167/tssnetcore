using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Sieve.Models;

namespace Tss.Core
{
    public interface IService
    {
    }

    public interface IService<T> : IService where T : class
    {
        List<T> Get(SieveModel sieveModel);

        Task<T> Get(object id);
        
        Task<int> Put(T entity);
        
        Task<int> Post(T entity);

        Task<int> Delete(object id);

        Task<int> Delete(T entity);        
    }
}