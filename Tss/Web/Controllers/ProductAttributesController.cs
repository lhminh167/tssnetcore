using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
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