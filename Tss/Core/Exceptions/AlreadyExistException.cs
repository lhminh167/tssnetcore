using System;
using Microsoft.AspNetCore.Http;
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
            var status = StatusCodes.Status400BadRequest;
            var response = new Response
            {
                Status = status,
                Title = base.Message,
                Detail = $"This (these) {name} already exist(s) in the database."
            };

            base.Data.Add("Response", response);
        }
    }
}
