using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;
using Tss.FunctionalTests.Web.Urls;
using Xunit;
using Xunit.Abstractions;

namespace Tss.FunctionalTests.Web.ControllerTests.Product.Recommendations
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

        [Fact, Trait("Priority", "5")]
        public async Task ReturnsOkResult()//done
        {
            var detailResponse = await new ProductRecommdationHelper(_fixture).Mock();
            var request = new ProductRecommedationRequest(_fixture.TestProduct.ProductId);

            var response = await _fixture.GetAsync<PaginatedList<Core.Entities.Product>>(
                new ProductUrl().Recommendation(request),
                System.Net.HttpStatusCode.OK
            );

            Assert.True(response.Content.Items.Any(e => e.ProductId == detailResponse.Content.ProductId));
        }
    }
}
