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
    public class Create
    {
        private readonly WebFixture _fixture;

        public Create(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "1")]
        public async Task ReturnsOkResult()//done
        {
            _fixture.SetAuthHeader(_fixture.TestCustomerLoginToken);
            var resquestBody = new ReviewCreateRequestHelper().Mock(_fixture.TestProduct.ProductId);

            var response = await _fixture.PostAsync<ReviewResponse>(
                new ReviewUrl().Index,
                resquestBody,
                HttpStatusCode.Created
            );

            var detailResponse = await _fixture.GetAsync<ReviewResponse>(
                new ReviewUrl().Detail(response.Content.ReviewId),
                HttpStatusCode.OK
            );
            Assert.True(detailResponse.Content.ReviewId > 0);
            Assert.True(detailResponse.Content.Review1 == resquestBody.Review1);
            Assert.True(detailResponse.Content.Rating == resquestBody.Rating);
        }
    }
}
