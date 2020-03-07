namespace Tss.Core.Models
{
    public class ReviewCreateRequest
    {
        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Review1 { get; set; }
        public short Rating { get; set; }
    }
}
