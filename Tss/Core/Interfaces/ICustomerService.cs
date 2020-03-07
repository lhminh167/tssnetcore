using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> Get();
        Task<Customer> Get(int customerId);
        Task<LoginResponse> Login(LoginRequest input);
        Task<LoginResponse> Create(CustomerCreateRequest input);
        Task<Customer> Update(CustomerUpdateRequest input);
        Task<Customer> Delete(int customerId);
    }
}