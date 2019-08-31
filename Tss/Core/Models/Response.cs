using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Constants;

namespace Tss.Core.Models
{
    internal class Response : ProblemDetails 
    {
        public object Content { get; set; }
    }
}