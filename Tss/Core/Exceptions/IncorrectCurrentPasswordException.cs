using System;
using Tss.Core.Models;
using System.Net;

namespace Tss.Core.Exceptions
{
    public class IncorrectCurrentPasswordException : Exception
    {
        public IncorrectCurrentPasswordException()
        : base(
            $"Incorrect current password. Please try again."
        )
        {
            Response response = new Response
            (
                HttpStatusCode.BadRequest,
                base.Message,
                $"You get this message because you provided incorrect password."
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
