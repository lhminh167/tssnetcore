using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class CustomerPasswordResetTokenGetResponse : CustomerPasswordResetTokenValidationRequest
    {
        public CustomerPasswordResetTokenGetResponse(
            Customer customer
        )
        : base(
            customer.CustomerId,
            customer.ResetPasswordToken
        )
        {
            Guard.Against.Null(customer, nameof(customer));
        }
    }
}
