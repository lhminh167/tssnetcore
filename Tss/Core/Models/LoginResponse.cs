using Ardalis.GuardClauses;
using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Models
{
    public class LoginResponse
    {
        public LoginResponse(Customer customer, IAuthHelper authHelper)
        {
            Guard.Against.Null(authHelper, nameof(authHelper));
            Guard.Against.Null(customer, nameof(customer));

            LoginToken = authHelper.GetLoginToken(customer);
            Customer = new CustomerResponse(customer);
        }

        public string LoginToken { get; private set; }
        public CustomerResponse Customer { get; private set; }
    }
}
