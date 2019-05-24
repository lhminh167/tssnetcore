using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
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