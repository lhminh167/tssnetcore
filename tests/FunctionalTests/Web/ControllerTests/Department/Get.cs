using System.Threading.Tasks;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;
using Tss.FunctionalTests.Web.Urls;
using Xunit;
using Xunit.Abstractions;

namespace Tss.FunctionalTests.Web.ControllerTests.Department
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
            var response = await _fixture.GetAsync<DepartmentResponse[]>(
                new DepartmentUrl().Index,
                System.Net.HttpStatusCode.OK
            );
        }
    }
}
