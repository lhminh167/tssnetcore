using FluentValidation;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ReviewGetRequestHelper
    {
        public ReviewGetRequest Mock(int productId)
        {
            return new ReviewGetRequest(productId);
        }



    }

}
