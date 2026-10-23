using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class LoginRequestHelper
    {
        public LoginRequest RootAdminAccount = new LoginRequest
        {
            Email = "lhminh167@gmail.com",
            Password = "string"
        };

        public LoginRequest Mock(CustomerCreateRequest input)
        {
            return new LoginRequest
            {
                Email = input.Email,
                Password = input.Password
            };
        }
    }
}
