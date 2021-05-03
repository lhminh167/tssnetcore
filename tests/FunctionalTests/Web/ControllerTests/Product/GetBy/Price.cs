using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tss.FunctionalTests.Web.ControllerTests.Product.GetBy
{
    [Collection("Startup collection")]
    public class Price
    {
        private readonly WebFixture _fixture;

        public Price(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "4")]
        public async Task ReturnsOkResult()//done
        {
            var price = _fixture.TestProduct.DiscountedPrice;
            var request = new ProductGetRequest(
                0, 8, true, null, true, null, null, null,
                new List<string> { },
                price - 0.5, price + 0.5, 0
            );

            var response = await _fixture.GetAsync<PaginatedList<Core.Entities.Product>>(
                new ProductUrl().List(request),
                HttpStatusCode.OK
            );

            Assert.True(response.Content.Items.Any(e => e.ProductId == _fixture.TestProduct.ProductId));
        }
    }
}
