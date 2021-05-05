using Tss.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IAttributeService
    {
        Task<List<Attribute>> Get();
        Task<Attribute> Get(int attributeId);
        Task<Attribute> Create(AttributeCreateRequest input);
        Task<Attribute> Update(AttributeUpdateRequest attribute);
        Task<Attribute> Delete(int attributeId);
    }
}