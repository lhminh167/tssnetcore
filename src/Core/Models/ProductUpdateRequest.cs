using System.Collections.Generic;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductUpdateRequest : ProductCreateRequest
    {
        public int ProductId { get; private set; }
    }
}
