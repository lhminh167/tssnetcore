using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Helpers;

namespace Tss.FunctionalTests.Web.ControllerTests.Product
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
            _fixture.SetAuthHeader(_fixture.TestAdminLoginToken);
            var resquestBody = new ProductCreateRequestHelper(_fixture).Mock();

            var response = await _fixture.PostAsync<ProductResponse>(
                new ProductUrl().Index,
                resquestBody,
                HttpStatusCode.Created
            );

            var detailResponse = await _fixture.GetAsync<ProductResponse>(
                new ProductUrl().Detail(response.Content.ProductId),
                HttpStatusCode.OK
            );
            Assert.True(detailResponse.Content.Name == resquestBody.Name);
        }
    }
}
