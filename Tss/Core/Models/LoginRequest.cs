using System.ComponentModel.DataAnnotations;

namespace Tss.Core.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
