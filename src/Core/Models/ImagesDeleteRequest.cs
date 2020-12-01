using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ImagesDeleteRequest
    {
        public ImagesDeleteRequest(List<ImageDeleteRequest> input)
        {
            Guard.Against.NullOrEmpty(input, nameof(input));

            Content = input;
        }

        public List<ImageDeleteRequest> Content { get; set; }
    }
}
