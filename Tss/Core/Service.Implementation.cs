using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Sieve.Models;

namespace Tss.Core
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;

        public Service(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual List<TEntity> Get(SieveModel sieveModel)
        {   
            return _repository.Get(sieveModel);
        }

        public virtual Task<TEntity> Get(object id)
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

        public virtual Task<int> Delete(object id)
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