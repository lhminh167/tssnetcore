using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Sieve.Models;
using Tss.Core.Generics;
using Sieve.Services;

namespace Tss.Infrastructure.Generics
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;
        private readonly SieveProcessor _sieveProcessor;

        public Service(
            IUnitOfWork unitOfWork, 
            IRepository<TEntity> repository,
            SieveProcessor sieveProcessor
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _sieveProcessor = sieveProcessor ?? throw new ArgumentNullException(nameof(sieveProcessor));
        }

        public virtual List<TEntity> Get(SieveModel sieveModel)
        {   
            IQueryable<TEntity> dbSet = _repository.Get();

            return _sieveProcessor.Apply<TEntity>(sieveModel, dbSet)
                .ToList();
        }

        public virtual Task<TEntity> Get(int id)
        {
            return _repository.Get(id);
        }

        public virtual Task<int> Put(TEntity entity)
        {
            _repository.Put(entity);
            return _unitOfWork.Save();
        }

        public virtual Task<int> Post(TEntity entity)
        {
            _repository.Post(entity);
            return _unitOfWork.Save();
        }

        public virtual Task<int> Delete(int id)
        {
            _repository.Delete(id);
            return _unitOfWork.Save();            
        }

        public virtual Task<int> Delete(TEntity entity)
        {
            _repository.Delete(entity);
            return _unitOfWork.Save();            
        }
    }
}