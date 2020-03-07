using System;
using Tss.Core.Models;
using System.Net;

namespace Tss.Core.Exceptions
{
    public class IncorrectLoginInfoException : Exception
    {
        public IncorrectLoginInfoException()
        : base(
            $"Incorrect email or password. Please try again."
        )
        {
            Response response = new Response
            (
                HttpStatusCode.BadRequest,
                base.Message,
                $"You get this message because you provided incorrect login info, or there is no user with such email."
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
