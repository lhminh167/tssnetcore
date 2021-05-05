using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
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

        [Required, Range(0, int.MaxValue)]
        public int CustomerId { get; set; }
        [Required]
        public string ResetPasswordToken { get; set; }
    }
}
