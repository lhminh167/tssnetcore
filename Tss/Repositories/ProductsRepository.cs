using Tss.Models;
using Tss.Core;
using Sieve.Services;

namespace Tss.Repositories
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