using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.ControllerTests.Catalog
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

        [Fact, Trait("Priority", "3")]
        public async Task ReturnsOkResult()//done
        {
            var response = await _fixture.GetAsync<CatalogGetResponse>(
                new CatalogUrl().List(new CatalogGetRequest(true, true, true, true)),
                HttpStatusCode.OK
            );

            Assert.NotEqual(response.Content.Role.Count, 0);
            Assert.NotEqual(response.Content.Shipping.Count, 0);
            Assert.NotEqual(response.Content.ShippingRegion.Count, 0);
            Assert.NotEqual(response.Content.Tax.Count, 0);
        }
    }
}
