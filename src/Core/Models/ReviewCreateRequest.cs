using System.ComponentModel.DataAnnotations;

namespace Tss.Core.Models
{
    public class ReviewCreateRequest
    {
        [Required, Range(0, int.MaxValue)]
        public int ProductId { get; set; }

        [Required, MinLength(50)]
        public string Review1 { get; set; }

        [Required, Range(1, 5)]
        public short Rating { get; set; }
    }
}
