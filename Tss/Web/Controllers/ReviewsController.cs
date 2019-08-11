using Tss.Infrastructure.Generics;
using Tss.Core.Generics;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    public class ReviewsController : Controller<Review>
    {
        public ReviewsController (
            IService<Review> service
        ): base(
            service,
            "ReviewId"
        ) { }
    }
}