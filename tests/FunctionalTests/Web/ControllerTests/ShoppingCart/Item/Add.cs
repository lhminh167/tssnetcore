using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Urls;
using System.Net;

namespace Tss.FunctionalTests.Web.ControllerTests.ShoppingCart.Item
{
    [Collection("Startup collection")]
    public class Add
    {
        private readonly WebFixture _fixture;

        public Add(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Theory, Trait("Priority", "1")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReturnsOkResult(bool buyNow)//done
        {
            var response = await new ShoppingCartItemAddRequestHelper(_fixture).Add(buyNow);
            
            _fixture.SetAuthHeader(response.Content.LoginToken);
            var cartResponse = await _fixture.GetAsync<ShoppingCartGetResponse>(
                new ShoppingCartUrl().List(new ShoppingCartGetRequestHelper().Mock(buyNow)),
                HttpStatusCode.OK
            );
            var item = cartResponse.Content.Items[0];
            Assert.Equal(cartResponse.Content.Count, 1);
            Assert.Equal(item.Attributes, response.Content.Item.Attributes);
            Assert.Equal(item.BuyNow, response.Content.Item.BuyNow);
            Assert.Equal(item.ProductId, response.Content.Item.ProductId);
            Assert.Equal(item.CartId, response.Content.Item.CartId);
        }
    }
}
