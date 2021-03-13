using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class CustomerPasswordResetRequest : CustomerPasswordResetTokenValidationRequest
    {
        public string NewPassword { get; set; }
    }

    public class CustomerPasswordResetRequestValidator : AbstractValidator<CustomerPasswordResetRequest>
    {
        public CustomerPasswordResetRequestValidator()
        {
            RuleFor(model => model.NewPassword).NotEmpty();
        }
    }
}
