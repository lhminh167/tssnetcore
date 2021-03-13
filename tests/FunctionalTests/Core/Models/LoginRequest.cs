using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(customer => customer.Email).NotEmpty().EmailAddress();
            RuleFor(customer => customer.Password).NotEmpty();
        }
    }
}
