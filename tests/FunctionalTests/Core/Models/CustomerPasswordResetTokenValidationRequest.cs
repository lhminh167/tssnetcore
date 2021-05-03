using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class CustomerPasswordResetTokenValidationRequest
    {
        public CustomerPasswordResetTokenValidationRequest(
        )
        {
        }

        public CustomerPasswordResetTokenValidationRequest(
            int customerId,
            string resetPasswordToken
        )
        {
            Guard.Against.OutOfRange(customerId, nameof(customerId), 0, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(resetPasswordToken, nameof(resetPasswordToken));

            CustomerId = customerId;
            ResetPasswordToken = resetPasswordToken;
        }

        public int CustomerId { get; set; }
        public string ResetPasswordToken { get; set; }
    }

    public class CustomerPasswordResetTokenValidationRequestValidator : AbstractValidator<CustomerPasswordResetTokenValidationRequest>
    {
        public CustomerPasswordResetTokenValidationRequestValidator()
        {
            RuleFor(model => model.CustomerId).GreaterThan(0);
            RuleFor(model => model.ResetPasswordToken).NotEmpty();
        }
    }
}
