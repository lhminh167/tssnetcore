using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ProductAttributesController : Controller<ProductAttribute>
    {
        public ProductAttributesController (
            IService<ProductAttribute> service
        ): base(
            service,
            "ProductAttributeId"
        ) { }
    }
}