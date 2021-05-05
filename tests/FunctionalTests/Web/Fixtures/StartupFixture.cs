using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
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

    [CollectionDefinition("Startup collection")]
    public class StartupCollection : ICollectionFixture<StartupFixture>
    {
    }
}
