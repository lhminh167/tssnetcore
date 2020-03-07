using System;
using System.Net;
using Tss.Core.Models;

namespace Tss.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name)
        : base(
            $"{name} not found."
        )
        {
            Response response = new Response
            (
                HttpStatusCode.NotFound,
                base.Message,
                $"This/these {name} does/do not exist in the database."
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
