using System.ComponentModel.DataAnnotations;

namespace Tss.Core.Models
{
    public class ReviewUpdateRequest
    {
        [Required, Range(0, int.MaxValue)]
        public int ReviewId { get; set; }

        [Required, MinLength(50)]
        public string Review1 { get; set; }

        [Required, Range(1, 5)]
        public short Rating { get; set; }
    }
}
