using System;
using Tss.Core.Models;
using System.Net;

namespace Tss.Core.Exceptions
{
    public class InvalidResetPasswordTokenException : Exception
    {
        public InvalidResetPasswordTokenException()
        : base(
            $"This password reset link is invalid or has expired."
        )
        {
            Response response = new Response
            (
                HttpStatusCode.BadRequest,
                base.Message,
                $"You get this message because you provided an invalid or expired password reset link."
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
