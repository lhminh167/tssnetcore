using System.Threading.Tasks;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;
using Tss.FunctionalTests.Web.Urls;
using Xunit;
using Xunit.Abstractions;

namespace Tss.FunctionalTests.Web.Controllers.Product
{
    public class GetHomepageProducts : IClassFixture<StartupFixture>
    {
        private readonly WebFixture _fixture;

        public GetHomepageProducts(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact]
        public async Task ReturnsOkResult()
        {
            var response = await _fixture.GetAsync<ProductsHomepage>(
                new ProductUrl().Homepage,
                System.Net.HttpStatusCode.OK
            );
        }
    }
}
