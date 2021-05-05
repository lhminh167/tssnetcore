using System.ComponentModel.DataAnnotations;

namespace Tss.Core.Models
{
    public class CustomerPasswordUpdateRequest
    {
        [Required, Range(0, int.MaxValue)]
        public int CustomerId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
