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
    public class Delete
    {
        private readonly WebFixture _fixture;

        public Delete(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "2")]
        public async Task ReturnsOkResult()//done
        {
            _fixture.SetAuthHeader(_fixture.TestAdminLoginToken);
            var createResquestBody = new ProductCreateRequestHelper(_fixture).Mock();
            var createResponse = await _fixture.PostAsync<ProductResponse>(
                new ProductUrl().Index,
                createResquestBody,
                HttpStatusCode.Created
            );

            var response = await _fixture.DeleteAsync<ProductResponse>(
                new ProductUrl().Detail(createResponse.Content.ProductId),
                null,
                HttpStatusCode.OK
            );

            var detailResponse = await _fixture.GetAsync<ProductResponse>(
                new ProductUrl().Detail(createResponse.Content.ProductId),
                HttpStatusCode.NotFound
            );
        }
    }
}
