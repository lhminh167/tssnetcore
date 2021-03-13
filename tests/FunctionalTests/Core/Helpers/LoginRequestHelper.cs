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
            Email = "lhminh.phs@gmail.com",
            Password = "string"
        };
    }
}
