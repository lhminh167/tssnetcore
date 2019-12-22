using System.ComponentModel.DataAnnotations;

namespace Tss.Core.Models
{
    public class LoginForm
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
