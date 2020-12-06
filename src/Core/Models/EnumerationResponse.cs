using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public abstract class EnumerationResponse
    {
        public string Message { get; set; }

        public int Id { get; set; }
    }
}