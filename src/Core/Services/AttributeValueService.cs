using Tss.Core.Entities;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Models;
using Ardalis.GuardClauses;

namespace Tss.Core.Services
{
    public partial class AttributeValueService : IAttributeValueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Attribute> _attributeRepo;
        private readonly IRepository<AttributeValue> _attributeValueRepo;
        private readonly IAuthHelper _authHelper;

        public AttributeValueService(
            IUnitOfWork unitOfWork,
            IRepository<Attribute> attributeRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _attributeRepo = attributeRepo ?? throw new System.ArgumentNullException(nameof(attributeRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new System.ArgumentNullException(nameof(attributeValueRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        public async Task<List<AttributeValue>> Get(string name)
        {
            return await _attributeValueRepo.Get(new GetAttributeValueByNameSpecification(name, _authHelper));
        }

        public async Task<AttributeValue> GetOne(int attributeValueId)
        {
            return await _attributeValueRepo.Get(attributeValueId);
        }

        public async Task<AttributeValue> Create(AttributeValueCreateRequest input)
        {
            AttributeValue dbAttributeValue = null;
            Guard.Against.NotFound(dbAttributeValue, nameof(AttributeValue));

            dbAttributeValue = null;
            _attributeValueRepo.Post(dbAttributeValue);
            await _unitOfWork.SaveAsync();
            return dbAttributeValue;
        }

        public async Task<AttributeValue> Update(AttributeValueUpdateRequest input)
        {
            AttributeValue dbAttributeValue = await _attributeValueRepo.Get(input.AttributeValueId);
            Guard.Against.NotFound(dbAttributeValue, nameof(AttributeValue));

            dbAttributeValue.Update(input);
            _attributeValueRepo.Put(dbAttributeValue);
            await _unitOfWork.SaveAsync();
            return dbAttributeValue;
        }

        public async Task<AttributeValue> Delete(int attributeValueId)
        {
            AttributeValue dbAttributeValue = await _attributeValueRepo.Get(attributeValueId);
            Guard.Against.NotFound(dbAttributeValue, nameof(AttributeValue));

            _attributeValueRepo.Delete(dbAttributeValue);
            await _unitOfWork.SaveAsync();
            return dbAttributeValue;
        }
    }
}