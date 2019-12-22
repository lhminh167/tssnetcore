using System;
using Microsoft.AspNetCore.Http;
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
            var status = StatusCodes.Status404NotFound;
            var response = new Response
            {
                Status = status,
                Title = base.Message,
                Detail = $"This/these {name} does/do not exist in the database."
            };

            base.Data.Add("Response", response);
        }
    }
}
