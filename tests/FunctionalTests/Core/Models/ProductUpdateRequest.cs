using System.Collections.Generic;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ProductUpdateRequest : ProductCreateRequest
    {
        public int ProductId { get; set; }
    }
}
