using System.Threading.Tasks;
using System;
using Tss.Models;

namespace Tss.Core
{
    public interface IUnitOfWork
    {
        void Register(IRepository repository);
        
        Task<int> Save();
    }
}