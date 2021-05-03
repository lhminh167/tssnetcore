using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Helpers;

namespace Tss.FunctionalTests.Web.ControllerTests.Review
{
    [Collection("Startup collection")]
    public class GetOne
    {
        private readonly WebFixture _fixture;

        public GetOne(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "4")]
        public async Task ReturnsOkResult()//done
        {
            _fixture.SetAuthHeader(_fixture.TestCustomerLoginToken);
            var createResquestBody = new ReviewCreateRequestHelper().Mock(_fixture.TestProduct.ProductId);
            var createResponse = await _fixture.PostAsync<ReviewResponse>(
                new ReviewUrl().Index,
                createResquestBody,
                HttpStatusCode.Created
            );

            var response = await _fixture.GetAsync<ReviewResponse>(
                new ReviewUrl().Detail(createResponse.Content.ReviewId),
                HttpStatusCode.OK
            );

            Assert.True(response.Content.ReviewId == createResponse.Content.ReviewId);
            Assert.True(response.Content.Review1 == createResquestBody.Review1);
            Assert.True(response.Content.Rating == createResquestBody.Rating);
        }
    }
}
