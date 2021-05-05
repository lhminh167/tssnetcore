using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;
using Tss.FunctionalTests.Web.Urls;
using Xunit;
using Xunit.Abstractions;

namespace Tss.FunctionalTests.Web.ControllerTests.ShoppingCart.Recommendations
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

        [Theory, Trait("Priority", "5")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReturnsOkResult(bool buyNow)//done
        {
            var detailResponse = await new ProductRecommdationHelper(_fixture).Mock();
            var createResponse = await new ShoppingCartItemAddRequestHelper(_fixture).Add(buyNow);
            _fixture.SetAuthHeader(createResponse.Content.LoginToken);

            var response = await _fixture.GetAsync<PaginatedList<Core.Entities.Product>>(
                new ShoppingCartUrl().Recommendation(new PaginationRequest()),
                System.Net.HttpStatusCode.OK
            );

            Assert.True(response.Content.Items.Any(e => e.ProductId == detailResponse.Content.ProductId));
        }
    }
}
