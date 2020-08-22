using Tss.Core.Entities;
using System;
using Tss.Core.Specifications;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;

namespace Tss.Core.Helpers
{
    public class ProductHelper : IProductHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _productRepo;
        private readonly IAuthHelper _authHelper;

        public ProductHelper(
            IUnitOfWork unitOfWork,
            IRepository<Product> productRepo,
            IAuthHelper authHelper,
            IMediaHelper mediaHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        public async Task<PaginatedList<Product>> Get(
            ProductGetRequest input
        )
        {
            return await _productRepo.GetPaginated(new GetProductSpecification(
                _authHelper,
                input
            ));
        }
    }
}