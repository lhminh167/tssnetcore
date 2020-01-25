using System;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Entities;
using Tss.Core.Models;
using System.Net;

namespace Tss.Core.Exceptions
{
    public class EmailAddressTakenException : Exception
    {
        public EmailAddressTakenException()
        : base(
            $"This email address is already taken. Please use another email address."
        )
        {
            Response response = new Response
            (
                HttpStatusCode.BadRequest,
                base.Message,
                $"This email address is already taken. Please use another email address."
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
