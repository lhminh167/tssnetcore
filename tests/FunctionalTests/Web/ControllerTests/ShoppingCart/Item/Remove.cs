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
    public class Remove
    {
        private readonly WebFixture _fixture;

        public Remove(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Theory, Trait("Priority", "2")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReturnsOkResult(bool buyNow)//done
        {
            var createResponse = await new ShoppingCartItemAddRequestHelper(_fixture).Add(buyNow);
            _fixture.SetAuthHeader(createResponse.Content.LoginToken);

            var response = await _fixture.DeleteAsync<ShoppingCartResponse>(
                new ShoppingCartUrl().Detail(createResponse.Content.Item.ItemId),
                null,
                HttpStatusCode.OK
            );

            var cartResponse = await _fixture.GetAsync<ShoppingCartGetResponse>(
                new ShoppingCartUrl().List(new ShoppingCartGetRequestHelper().Mock(buyNow)),
                HttpStatusCode.OK
            );
            Assert.Equal(cartResponse.Content.Count, 0);
        }
    }
}
