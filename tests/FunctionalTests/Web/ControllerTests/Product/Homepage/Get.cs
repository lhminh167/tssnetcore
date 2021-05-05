using System.Threading.Tasks;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;
using Tss.FunctionalTests.Web.Urls;
using Xunit;
using Xunit.Abstractions;

namespace Tss.FunctionalTests.Web.ControllerTests.Product.Homepage
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

        [Fact, Trait("Priority", "4")]
        public async Task ReturnsOkResult()//done
        {
            var response = await _fixture.GetAsync<ProductsHomepage>(
                new ProductUrl().Homepage,
                System.Net.HttpStatusCode.OK
            );
            Assert.True(response.Content.TopPanel.Items.Count > 0);
            Assert.True(response.Content.TopRated.Items.Count > 0);
            Assert.True(response.Content.Newest.Items.Count > 0);
            Assert.True(response.Content.MostDiscounted.Items.Count > 0);
            Assert.True(response.Content.BestSelling.Items.Count > 0);
        }
    }
}
