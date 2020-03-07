using System.Threading.Tasks;

namespace Tss.Core.Interfaces
{
    public interface IUnitOfWork
    {
        void Register(IRepository repository);
        
        Task<int> SaveAsync();

        void Rollback();
    }
}
