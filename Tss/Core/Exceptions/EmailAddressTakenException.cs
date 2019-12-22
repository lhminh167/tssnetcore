using System;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Exceptions
{
    public class EmailAddressTakenException : Exception
    {
        public EmailAddressTakenException()
        : base(
            $"This email address is already taken. Please use another email address."
        )
        {
            var status = StatusCodes.Status400BadRequest;
            var response = new Response
            {
                Status = status,
                Title = base.Message,
                Detail = $"This email address is already taken. Please use another email address."
            };

            base.Data.Add("Response", response);
        }
    }
}
