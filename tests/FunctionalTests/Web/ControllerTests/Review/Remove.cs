using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Urls;
using System.Net;

namespace Tss.FunctionalTests.Web.ControllerTests.Review
{
    [Collection("Startup collection")]
    public class Remove
    {
        private readonly WebFixture _fixture;

        public Remove(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "2")]
        public async Task ReturnsOkResult()//done
        {
            _fixture.SetAuthHeader(_fixture.TestCustomerLoginToken);
            var createResquestBody = new ReviewCreateRequestHelper().Mock(_fixture.TestProduct.ProductId);
            var createResponse = await _fixture.PostAsync<ReviewResponse>(
                new ReviewUrl().Index,
                createResquestBody,
                HttpStatusCode.Created
            );

            var response = await _fixture.DeleteAsync<ReviewResponse>(
                new ReviewUrl().Detail(createResponse.Content.ReviewId),
                null,
                HttpStatusCode.OK
            );

            var detailResponse = await _fixture.GetAsync<ReviewResponse>(
                new ReviewUrl().Detail(createResponse.Content.ReviewId),
                HttpStatusCode.NotFound
            );
        }
    }
}
