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
            return await _attributeRepo.Get(new GetAttributeSpecification(_authHelper));
        }

        public async Task<Attribute> Get(int attributeId)
        {
            return await _attributeRepo.GetOne(new GetAttributeByIdSpecification(attributeId, _authHelper));
        }

        public async Task<Attribute> Create(AttributeCreateRequest input)
        {
            Attribute attribute = await _attributeRepo.GetOne(new GetAttributeByNameSpecification(input.Name, _authHelper));
            Guard.Against.AlreadyExist(attribute, nameof(Attribute));

            attribute = new Attribute(input.Name);
            _attributeRepo.Post(attribute);
            await _unitOfWork.SaveAsync();
            return attribute;
        }

        public async Task<Attribute> Update(Attribute input)
        {
            Attribute attribute = await _attributeRepo.Get(input.AttributeId);
            Guard.Against.NotFound(attribute, nameof(Attribute));

            attribute.Update(input.Name);
            _attributeRepo.Put(attribute);
            await _unitOfWork.SaveAsync();
            return attribute;
        }

        public async Task<Attribute> Delete(int attributeId)
        {
            Attribute attribute = await _attributeRepo.Get(attributeId);
            Guard.Against.NotFound(attribute, nameof(Attribute));

            _attributeRepo.Delete(attribute);
            await _unitOfWork.SaveAsync();
            return attribute;
        }
    }
}