using Tss.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IAttributeValueService
    {
        Task<List<AttributeValue>> Get(string name);
        Task<AttributeValue> GetOne(int attributeValueId);
        Task<AttributeValue> Create(AttributeValueCreateRequest input);
        Task<AttributeValue> Update(AttributeValueUpdateRequest attributeValue);
        Task<AttributeValue> Delete(int attributeValueId);
    }
}