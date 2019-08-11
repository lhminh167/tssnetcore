using System.Threading.Tasks;
using System.Linq;

namespace Tss.Core.Generics
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : class
    {
        IQueryable<T> Get();

        Task<T> Get(int id);
        
        void Put(T entity);
        
        void Post(T entity);

        void Delete(int id);

        void Delete(T entity);        
    }
}
