using Ardalis.GuardClauses;
using FluentValidation;
using Tss.FunctionalTests.Core.Models;
using Tss.Core.Interfaces;

namespace Tss.FunctionalTests.Core.Models
{
    public class LoginResponse
    {
        public LoginResponse()
        {
        }

        public string LoginToken { get; set; }
        public CustomerResponse Customer { get; set; }
    }

    public class LoginResponseValidator : AbstractValidator<LoginResponse>
    {
        public LoginResponseValidator()
        {
            RuleFor(customer => customer.LoginToken).NotEmpty();
            RuleFor(customer => customer.Customer).NotNull();
        }
    }
}
