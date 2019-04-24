using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ProductAttributeController : Controller<ProductAttribute>
    {
        public ProductAttributeController (
            IService<ProductAttribute> service
        ): base(
            service,
            "ProductAttributeId"
        ) { }
    }
}