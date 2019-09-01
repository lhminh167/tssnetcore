using System.Collections.Generic;
using System.Linq;
using Sieve.Models;
using Tss.Core.Specifications;
using Tss.Core.Models;

namespace Tss.Core.Controllers
{
    public interface IProductFilterController
    {
        ProductFilterFilter Filter();
        
        ProductFilterProduct Product(
            ProductFilterProductSpecifications specs
        );
    }
}
