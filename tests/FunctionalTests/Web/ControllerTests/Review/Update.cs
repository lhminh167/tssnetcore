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
    public class Update
    {
        private readonly WebFixture _fixture;

        public Update(
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
            var resquestBody = new ReviewUpdateRequestHelper().Mock(createResponse.Content.ReviewId);

            var response = await _fixture.PutAsync<ReviewResponse>(
                new ReviewUrl().Detail(createResponse.Content.ReviewId),
                resquestBody,
                HttpStatusCode.OK
            );

            var detailResponse = await _fixture.GetAsync<ReviewResponse>(
                new ReviewUrl().Detail(createResponse.Content.ReviewId),
                HttpStatusCode.OK
            );
            Assert.Equal(detailResponse.Content.Review1, resquestBody.Review1);
            Assert.Equal(detailResponse.Content.Rating, resquestBody.Rating);
        }
    }
}
