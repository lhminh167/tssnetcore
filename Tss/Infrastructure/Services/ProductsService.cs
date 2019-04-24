using Tss.Core;

namespace Tss.Infrastructure
{
    public class ProductsService : Service<Product>
    {
        public ProductsService (
            IUnitOfWork unitOfWork, 
            IRepository<Product> repository
        ): base(
            unitOfWork, 
            repository
        ) { }
    }
}