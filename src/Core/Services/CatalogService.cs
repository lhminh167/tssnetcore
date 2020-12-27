using Tss.Core.Entities;
using System;
using Tss.Core.Specifications;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;

namespace Tss.Core.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IRepository<Shipping> _shippingRepo;
        private readonly IRepository<ShippingRegion> _shippingRegionRepo;
        private readonly IRepository<Tax> _taxRepo;
        private readonly IRepository<AttributeValue> _attributeValueRepo;
        private readonly IAuthHelper _authHelper;

        public CatalogService(
            IRepository<Shipping> shippingRepo,
            IRepository<ShippingRegion> shippingRegionRepo,
            IRepository<Tax> taxRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IAuthHelper authHelper
        )
        {
            _shippingRepo = shippingRepo ?? throw new ArgumentNullException(nameof(shippingRepo));
            _shippingRegionRepo = shippingRegionRepo ?? throw new ArgumentNullException(nameof(shippingRegionRepo));
            _taxRepo = taxRepo ?? throw new ArgumentNullException(nameof(taxRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new ArgumentNullException(nameof(attributeValueRepo));
            _authHelper = authHelper ?? throw new ArgumentNullException(nameof(authHelper));
        }

        public async Task<CatalogGetResponse> Get(CatalogGetRequest input)
        {
            CatalogGetResponse response = new CatalogGetResponse();

            if (input.Shipping) response.SetShipping(await _shippingRepo.Get());
            if (input.ShippingRegion) response.SetShippingRegion(await _shippingRegionRepo.Get());
            if (input.Tax) response.SetTax(await _taxRepo.Get());
            if (input.Role) response.SetRole(await _attributeValueRepo.Get(new GetAttributeValueByNameSpecification("Role", _authHelper)));

            return response;
        }
    }
}