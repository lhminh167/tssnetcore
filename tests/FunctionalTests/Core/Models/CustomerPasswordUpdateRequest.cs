using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class CustomerPasswordUpdateRequest
    {
        public int CustomerId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }

    public class CustomerPasswordUpdateRequestValidator : AbstractValidator<CustomerPasswordUpdateRequest>
    {
        public CustomerPasswordUpdateRequestValidator()
        {
            RuleFor(model => model.CustomerId).GreaterThan(0);
            RuleFor(model => model.Password).NotEmpty();
            RuleFor(model => model.NewPassword).NotEmpty();
        }
    }
}
