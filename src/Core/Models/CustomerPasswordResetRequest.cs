using System.ComponentModel.DataAnnotations;

namespace Tss.Core.Models
{
    public class CustomerPasswordResetRequest : CustomerPasswordResetTokenValidationRequest
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
