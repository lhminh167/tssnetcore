using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using Sieve.Models;

namespace Tss.Core
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : class
    {
        List<T> Get(SieveModel sieveModel);

        Task<T> Get(object id);
        
        void Put(T entity);
        
        void Post(T entity);

        void Delete(object id);

        void Delete(T entity);        
    }
}
