using System.Collections.Generic;
using System.Linq;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class CatalogGetResponse
    {
        public CatalogGetResponse()
        {
        }

        public List<ShippingResponse> Shipping { get; set; }
        public List<ShippingRegionResponse> ShippingRegion { get; set; }
        public List<TaxResponse> Tax { get; set; }
        public List<AttributeValueResponse> Role { get; set; }
    }
}