using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Models;

namespace Tss.Core.Services
{
    public interface IProductFilterService
    {
        ProductFilterFilter Filter();

        List<ProductFilterProduct> Product(
            ProductFilterProductSpecifications specs
        );
    }
}
