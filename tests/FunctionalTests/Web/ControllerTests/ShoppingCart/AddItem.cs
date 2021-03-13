using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Urls;
using System.Net;

namespace Tss.FunctionalTests.Web.Controllers
{
    public class AddItem : IClassFixture<StartupFixture>
    {
        private readonly WebFixture _fixture;

        public AddItem(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact]
        public async Task ReturnsOkResult()
        {
            var body = new ShoppingCartItemAddRequestHelper().Mock(_fixture.TestProduct, 1, true);
            var response = await _fixture.PostAsync<ShoppingCartItemAddResponse>(
                new ShoppingCartUrl().Index,
                body,
                HttpStatusCode.Created
            );
            _fixture.SetAuthHeader(response.Content.LoginToken);
            var cartResponse = await _fixture.GetAsync<ShoppingCartGetResponse>(
                new ShoppingCartUrl().List(new ShoppingCartGetRequestHelper().Mock(true)),
                HttpStatusCode.OK
            );
            Assert.Equal(1, cartResponse.Content.Count);
        }
    }
}
