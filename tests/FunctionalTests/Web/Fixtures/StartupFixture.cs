using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Tss.FunctionalTests.Web.Fixtures
{
    public class StartupFixture : WebApplicationFactory<Startup>
    {
        public WebFixture CreateClient(ITestOutputHelper output)
        {
            return new WebFixture(this, output);
        }
    }
}
