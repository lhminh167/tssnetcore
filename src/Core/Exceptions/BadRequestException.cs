using System;
using System.Net;
using Tss.Core.Models;

namespace Tss.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string massage)
        : base(
            massage
        )
        {
            Response response = new Response
            (
                HttpStatusCode.BadRequest,
                base.Message,
                $"Bad request. Your input does not satisfy validation requirements."
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
