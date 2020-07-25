using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> Get();
        Task<CustomerResponse> Get(int customerId);
        Task<LoginResponse> Login(LoginRequest input);
        Task<LoginResponse> Create(CustomerCreateRequest input);
        Task<CustomerResponse> Update(CustomerUpdateRequest input);
        Task<CustomerResponse> UpdatePassword(CustomerPasswordUpdateRequest input);
        Task<CustomerResponse> Delete(int customerId);
    }
}