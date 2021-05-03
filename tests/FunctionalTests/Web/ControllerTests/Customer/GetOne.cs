using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Entities;

namespace Tss.FunctionalTests.Web.ControllerTests.Customer
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

        [Theory, Trait("Priority", "3")]
        [InlineData("Customer")]
        [InlineData("Admin")]
        public async Task ReturnsOkResult(string role)//done
        {
            _fixture.SetAuthHeader(
                role == Role.Customer.Message ?
                _fixture.TestCustomerLoginToken :
                _fixture.TestAdminLoginToken
            );

            var detailResponse = await _fixture.GetAsync<CustomerResponse>(
                new CustomerUrl().Detail(_fixture.TestCustomer.CustomerId),
                HttpStatusCode.OK
            );

            Assert.Equal(detailResponse.Content.Name, _fixture.TestCustomer.Name);
        }
    }
}
