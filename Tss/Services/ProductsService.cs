using Tss.Models;
using Tss.Core;

namespace Tss.Services
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