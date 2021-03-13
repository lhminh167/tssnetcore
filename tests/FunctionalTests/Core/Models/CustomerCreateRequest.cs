using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class CustomerCreateRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CustomerCreateRequestValidator : AbstractValidator<CustomerCreateRequest>
    {
        public CustomerCreateRequestValidator()
        {
            RuleFor(customer => customer.Name).NotEmpty();
            RuleFor(customer => customer.Email).NotEmpty().EmailAddress();
            RuleFor(customer => customer.Password).NotEmpty();
        }
    }
}
