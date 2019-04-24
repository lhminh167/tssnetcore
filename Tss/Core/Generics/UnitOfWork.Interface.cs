using System.Threading.Tasks;
using System;

namespace Tss.Core
{
    public interface IUnitOfWork
    {
        void Register(IRepository repository);
        
        Task<int> Save();
    }
}