using System;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ImageDeleteRequest
    {
        public ImageDeleteRequest(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            Name = name;
        }

        public string Name { get; private set; }
    }
}
