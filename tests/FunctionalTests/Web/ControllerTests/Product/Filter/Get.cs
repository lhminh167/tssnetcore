using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.ControllerTests.Product.Filter
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
        public void ReturnsOkResult()//done
        {
            Assert.NotEmpty(_fixture.TestProductFilter.Attributes);
            Assert.NotEmpty(_fixture.TestProductFilter.Categories);
        }
    }
}
