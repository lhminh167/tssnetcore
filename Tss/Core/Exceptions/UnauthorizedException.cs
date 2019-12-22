using System;
using Microsoft.AspNetCore.Http;
using Tss.Core.Models;

namespace Tss.Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        : base(
            "Access denied. Please log in or ask an admin to grant you permission to access this resource."
        )
        {
            var status = StatusCodes.Status401Unauthorized;
            var response = new Response
            {
                Status = status,
                Title = base.Message,
                Detail = $"You are not logged in or do not have permission to access this resource."
            };

            base.Data.Add("Response", response);
        }
    }
}
