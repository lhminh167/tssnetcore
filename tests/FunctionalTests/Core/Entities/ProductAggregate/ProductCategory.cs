namespace Tss.FunctionalTests.Core.Entities
{
    public class ProductCategory
    {
        public ProductCategory(int productId, int categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }

        public ProductCategory()
        {
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
