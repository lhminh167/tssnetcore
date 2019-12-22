using Tss.Core.Entities;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Models;
using Ardalis.GuardClauses;

namespace Tss.Core.Services
{
    public partial class AttributeService : IAttributeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Attribute> _attributeRepo;
        private readonly IAuthHelper _authHelper;

        public AttributeService(
            IUnitOfWork unitOfWork,
            IRepository<Attribute> attributeRepo,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _attributeRepo = attributeRepo ?? throw new System.ArgumentNullException(nameof(attributeRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        public async Task<List<Attribute>> Get()
        {
            return await _attributeRepo.Get(new GetAttributeByNameSpecification(null, _authHelper));
        }

        public async Task<Attribute> Get(int attributeId)
        {
            return await _attributeRepo.Get(attributeId);
        }

        public async Task<Attribute> Create(AttributeCreateRequest input)
        {
            Attribute dbAttribute = await _attributeRepo.GetOne(new GetAttributeByNameSpecification(input.Name, _authHelper));
            Guard.Against.AlreadyExist(dbAttribute, nameof(Attribute));

            dbAttribute = new Attribute(input.Name);
            _attributeRepo.Post(dbAttribute);
            await _unitOfWork.SaveAsync();
            return dbAttribute;
        }

        public async Task<Attribute> Update(Attribute attribute)
        {
            Attribute dbAttribute = await _attributeRepo.Get(attribute.AttributeId);
            Guard.Against.NotFound(dbAttribute, nameof(Attribute));

            _attributeRepo.Put(dbAttribute);
            await _unitOfWork.SaveAsync();
            return dbAttribute;
        }

        public async Task<Attribute> Delete(int attributeId)
        {
            Attribute dbAttribute = await _attributeRepo.Get(attributeId);
            Guard.Against.NotFound(dbAttribute, nameof(Attribute));

            _attributeRepo.Delete(dbAttribute);
            await _unitOfWork.SaveAsync();
            return dbAttribute;
        }
    }
}