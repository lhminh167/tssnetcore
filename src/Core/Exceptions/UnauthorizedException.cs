using System;
using System.Net;
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
            Response response = new Response
            (
                HttpStatusCode.Unauthorized,
                base.Message,
                $"You are not logged in or do not have permission to access this resource."
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
