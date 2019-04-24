using Tss.Infrastructure;
using Tss.Core;

namespace Tss.Web
{
    public class ReviewController : Controller<Review>
    {
        public ReviewController (
            IService<Review> service
        ): base(
            service,
            "ReviewId"
        ) { }
    }
}