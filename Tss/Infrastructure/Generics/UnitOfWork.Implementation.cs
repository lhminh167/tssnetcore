using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using Tss.Core;

namespace Tss.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TssContext _context;
        private readonly Dictionary<string, IRepository> _repositories;

        public UnitOfWork (
            TssContext context
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repositories = new Dictionary<string, IRepository>();
        }

        public void Register(IRepository repository)
        {
            _repositories.Add(repository.GetType().Name, repository);
        }

        public Task<int> Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}