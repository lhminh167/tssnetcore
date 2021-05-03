using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.ControllerTests.Product
{
    [Collection("Startup collection")]
    public class GetOne
    {
        private readonly WebFixture _fixture;

        public GetOne(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "3"), Trait("IsMostSimpleTest", "True")]
        public async Task ReturnsOkResult()//done
        {
            var response = await _fixture.GetAsync<ProductResponse>(
                new ProductUrl().Detail(_fixture.TestProduct.ProductId),
                HttpStatusCode.OK
            );

            Assert.Equal(response.Content.Name, _fixture.TestProduct.Name);
        }
    }
}
