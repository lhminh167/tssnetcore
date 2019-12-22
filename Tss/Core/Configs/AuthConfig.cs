using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Tss.Core.Interfaces;

namespace Tss.Core.Configs
{
    public static class AuthConfig
    {
        public static Task OnTokenValidated(TokenValidatedContext context)
        {
            var customerService = context.HttpContext.RequestServices.GetRequiredService<ICustomerService>();
            var customerId = int.Parse(context.Principal.Identity.Name);
            var customer = customerService.Get(customerId);
            if (customer == null)
            {
                // return unauthorized if customer no longer exists
                context.Fail("Unauthorized");
            }
            return Task.CompletedTask;
        }

        public static Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            context.Fail("Unauthorized");
            
            return Task.CompletedTask;
        }

    }
}
