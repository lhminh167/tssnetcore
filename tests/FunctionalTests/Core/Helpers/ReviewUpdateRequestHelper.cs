using FluentValidation;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ReviewUpdateRequestHelper
    {
        public ReviewUpdateRequest Mock(int reviewId)
        {
            var randomText = new RandomGeneratorHelper().RandomString(50);
            return new ReviewUpdateRequest
            {
                ReviewId = reviewId,
                Review1 = randomText,
                Rating = 3,
            };
        }



    }

}
