using Tss.Core.Entities;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Models;
using Ardalis.GuardClauses;

namespace Tss.Core.Services
{
    public partial class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Department> _DepartmentRepo;
        private readonly IAuthHelper _authHelper;

        public DepartmentService(
            IUnitOfWork unitOfWork,
            IRepository<Department> DepartmentRepo,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _DepartmentRepo = DepartmentRepo ?? throw new System.ArgumentNullException(nameof(DepartmentRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        public async Task<List<Department>> Get()
        {
            return await _DepartmentRepo.Get(new GetDepartmentSpecification(_authHelper));
        }

        // public async Task<Department> Get(int DepartmentId)
        // {
        //     return await _DepartmentRepo.GetOne(new GetDepartmentByIdSpecification(DepartmentId, _authHelper));
        // }

        // public async Task<Department> Create(DepartmentCreateRequest input)
        // {
        //     Department Department = await _DepartmentRepo.GetOne(new GetDepartmentByNameSpecification(input.Name, _authHelper));
        //     Guard.Against.AlreadyExist(Department, nameof(Department));

        //     Department = new Department(input.Name);
        //     _DepartmentRepo.Post(Department);
        //     await _unitOfWork.SaveAsync();
        //     return Department;
        // }

        // public async Task<Department> Update(Department input)
        // {
        //     Department Department = await _DepartmentRepo.Get(input.DepartmentId);
        //     Guard.Against.NotFound(Department, nameof(Department));

        //     Department.Update(input.Name);
        //     _DepartmentRepo.Put(Department);
        //     await _unitOfWork.SaveAsync();
        //     return Department;
        // }

        // public async Task<Department> Delete(int DepartmentId)
        // {
        //     Department Department = await _DepartmentRepo.Get(DepartmentId);
        //     Guard.Against.NotFound(Department, nameof(Department));

        //     _DepartmentRepo.Delete(Department);
        //     await _unitOfWork.SaveAsync();
        //     return Department;
        // }
    }
}