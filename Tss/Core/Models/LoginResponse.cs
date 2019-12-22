using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class LoginResponse
    {
        public LoginResponse(string token)
        {
            Guard.Against.Null(token, nameof(token));
            
            Token = token;
        }

        public string Token { get; set; }
    }
}
