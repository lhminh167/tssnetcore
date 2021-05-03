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
    public class Get
    {
        private readonly WebFixture _fixture;

        public Get(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "4")]
        public async Task ReturnsOkResult()//done
        {
            _fixture.SetAuthHeader(_fixture.TestAdminLoginToken);
            var productResquestBody = new ProductCreateRequestHelper(_fixture).Mock();
            var productResponse = await _fixture.PostAsync<ProductResponse>(
                new ProductUrl().Index,
                productResquestBody,
                HttpStatusCode.Created
            );
            _fixture.SetAuthHeader(_fixture.TestCustomerLoginToken);
            var createResquestBody = new ReviewCreateRequestHelper().Mock(productResponse.Content.ProductId);
            var createResponse = await _fixture.PostAsync<ReviewResponse>(
                new ReviewUrl().Index,
                createResquestBody,
                HttpStatusCode.Created
            );
            var request = new ReviewGetRequestHelper().Mock(productResponse.Content.ProductId);

            var response = await _fixture.GetAsync<ReviewGetResponse>(
                new ReviewUrl().List(request),
                HttpStatusCode.OK
            );

            var review = response.Content.Items[0];
            Assert.True(response.Content.Count == 1);
            Assert.True(review.ReviewId == createResponse.Content.ReviewId);
        }
    }
}
