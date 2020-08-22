using System;
using System.Net;
using Tss.Core.Models;

namespace Tss.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(Response input)
        : base(
            input.Title
        )
        {
            Response response = new Response
            (
                input.Status,
                base.Message,
                input.Title
            );
            base.Data.Add(nameof(Response), response);
        }
    }
}
