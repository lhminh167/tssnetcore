using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Urls;
using System.Net;

namespace Tss.FunctionalTests.Web.ControllerTests.ShoppingCart.Item.Quantity
{
    [Collection("Startup collection")]
    public class Total
    {
        private readonly WebFixture _fixture;

        public Total(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Theory, Trait("Priority", "4")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReturnsOkResult(bool buyNow)//done
        {
            var createResponse = await new ShoppingCartItemAddRequestHelper(_fixture).Add(buyNow);
            _fixture.SetAuthHeader(createResponse.Content.LoginToken);

            var response = await _fixture.GetAsync<int>(
                new ShoppingCartUrl().GetTotalQuantity(buyNow),
                HttpStatusCode.OK
            );

            Assert.Equal(response.Content, 1);
        }
    }
}
