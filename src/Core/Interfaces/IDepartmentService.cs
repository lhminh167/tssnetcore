using Tss.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> Get();
        // Task<Department> Get(int DepartmentId);
        // Task<Department> Create(DepartmentCreateRequest input);
        // Task<Department> Update(Department Department);
        // Task<Department> Delete(int DepartmentId);
    }
}