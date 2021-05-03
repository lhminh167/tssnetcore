using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.ControllerTests.Category
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

        [Fact, Trait("Priority", "3")]
        public async Task ReturnsOkResult()//done
        {
            var response = await _fixture.GetAsync<ProductResponse>(
                new CategoryUrl().Detail(1),
                HttpStatusCode.OK
            );
        }
    }
}
