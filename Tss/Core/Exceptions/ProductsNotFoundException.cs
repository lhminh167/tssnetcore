using Tss.Core.Entities;

namespace Tss.Core.Exceptions
{
    public class ProductsNotFoundException : ProductsApiException
    {
        public ProductsNotFoundException(Product product)
            : base($"Product {product.Name} was found.")
        {
        }
    }
}