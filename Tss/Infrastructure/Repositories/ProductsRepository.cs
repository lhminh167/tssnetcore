using Tss.Core;
using Sieve.Services;

namespace Tss.Infrastructure
{
    public class ProductsRepository : Repository<Product>
    {
        public ProductsRepository(
            TssContext context,
            IUnitOfWork unitOfWork,
            SieveProcessor sieveProcessor
        ): base(
            context,
            unitOfWork,
            sieveProcessor
        ) { }
    }
}