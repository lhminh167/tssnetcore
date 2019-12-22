using System;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Exceptions
{
    public class IncorrectLoginInfoException : Exception
    {
        public IncorrectLoginInfoException()
        : base(
            $"Incorrect email or password. Please try again."
        )
        {
            var status = StatusCodes.Status400BadRequest;
            var response = new Response
            {
                Status = status,
                Title = base.Message,
                Detail = $"You get this message because you provided incorrect login info, or there is no user with such email."
            };

            base.Data.Add("Response", response);
        }
    }
}
