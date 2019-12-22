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
        Task<LoginResponse> Login(LoginForm input);
        Task<Customer> Create(RegistrationForm input);
        Task<Customer> Update(RegistrationForm input);
        Task<Customer> Delete(int customerId);
    }
}