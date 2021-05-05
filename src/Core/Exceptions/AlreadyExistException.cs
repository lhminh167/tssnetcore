using System;
using System.Net;
using Tss.Core.Models;

namespace Tss.Core.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string name)
        : base(
            $"{name} already exist(s)."
        )
        {
            Response response = new Response
            (
                HttpStatusCode.BadRequest,
                base.Message,
                $"This (these) {name} already exist(s) in the database."
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
