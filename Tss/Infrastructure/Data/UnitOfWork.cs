using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Tss.Core.Interfaces;
using Tss.Infrastructure.Data;
using System.Linq;

namespace Tss.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TssContext _context;
        private readonly Dictionary<string, IRepository> _repositories;

        public UnitOfWork(
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

        public Task<int> SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.Rollback();
                throw ex;
            }
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

    }
}
