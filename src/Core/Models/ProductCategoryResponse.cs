namespace Tss.Core.Models
{
    public class ProductCategoryResponse
    {
        public ProductCategoryResponse()
        {
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual CategoryResponse Category { get; set; }
        public virtual ProductResponse Product { get; set; }
    }
}
