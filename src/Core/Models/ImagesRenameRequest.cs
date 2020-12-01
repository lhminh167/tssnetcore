using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ImagesRenameRequest
    {
        public ImagesRenameRequest(List<ImageRenameRequest> input)
        {
            Guard.Against.NullOrEmpty(input, nameof(input));

            Content = input;
        }

        public List<ImageRenameRequest> Content { get; set; }
    }
}
