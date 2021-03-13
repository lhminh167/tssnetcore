using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.Controllers.Product
{
    public class GetOne : IClassFixture<StartupFixture>
    {
        private readonly WebFixture _fixture;

        public GetOne(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact]
        public async Task ReturnsOkResult()
        {
            var response = await _fixture.GetAsync<ProductResponse>(
                new ProductUrl().Detail(_fixture.TestProduct.ProductId),
                HttpStatusCode.OK
            );
        }
    }
}
