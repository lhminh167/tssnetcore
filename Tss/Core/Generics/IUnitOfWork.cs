using System.Threading.Tasks;

namespace Tss.Core.Generics
{
    public interface IUnitOfWork
    {
        void Register(IRepository repository);
        
        Task<int> Save();
    }
}