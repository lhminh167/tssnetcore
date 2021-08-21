namespace Tss.Core.Entities
{
    public class ProductCategory
    {
        public ProductCategory(int productId, int categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }

        private ProductCategory()
        {
        }

        public int ProductId { get; private set; }
        public int CategoryId { get; private set; }

        public virtual Category Category { get; private set; }
        public virtual Product Product { get; private set; }
    }
}
