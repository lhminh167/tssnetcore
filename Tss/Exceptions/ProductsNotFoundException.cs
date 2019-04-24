using Tss.Models;

namespace Tss
{
    public class ProductsNotFoundException : ProductsApiException
    {
        public ProductsNotFoundException(Product product)
            : base($"Product {product.Name} was found.")
        {
        }
    }
}