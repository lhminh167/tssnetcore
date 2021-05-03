using FluentValidation;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ReviewCreateRequestHelper
    {
        public ReviewCreateRequest Mock(int productId)
        {
            var randomText = new RandomGeneratorHelper().RandomString(50);
            return new ReviewCreateRequest
            {
                ProductId = productId,
                Review1 = randomText,
                Rating = 3,
            };
        }



    }

}
