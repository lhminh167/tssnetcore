using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Urls;
using System.Net;
using System;

namespace Tss.FunctionalTests.Web.ControllerTests.Order.Form
{
    [Collection("Startup collection")]
    public class Get
    {
        private readonly WebFixture _fixture;
        private readonly ITestOutputHelper _output;

        public Get(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
            _output = output;
        }

        [Fact, Trait("Priority", "3")]
        public async Task ReturnsOkResult()//done
        {
            _fixture.SetAuthHeader(_fixture.TestCustomerLoginToken);

            var formResponse = await _fixture.GetAsync<OrderCreateFormResponse>(
                new OrderUrl().New,
                HttpStatusCode.OK
            );

            Assert.True(formResponse.Content.ShippingRegions.Count > 0);
            Assert.True(formResponse.Content.Shippings.Count > 0);
            Assert.True(formResponse.Content.Taxes.Count > 0);
        }
    }
}
