using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tss.FunctionalTests.Core.Models
{
    public class ImageRenameRequest
    {
        public ImageRenameRequest(string oldName, string newName)
        {
            Guard.Against.NullOrWhiteSpace(oldName, nameof(OldName));
            Guard.Against.NullOrWhiteSpace(newName, nameof(NewName));
            Guard.Against.BadRequest(newName == oldName, "Image's new image should be different than image's old name.");

            OldName = oldName;
            NewName = newName;
        }

        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
