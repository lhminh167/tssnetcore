using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Helpers;

namespace Tss.FunctionalTests.Web.ControllerTests.Customer
{
    [Collection("Startup collection")]
    public class Login
    {
        private readonly WebFixture _fixture;

        public Login(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "1")]
        public async Task ReturnsOkResult()//done
        {
            var createRequest = new CustomerCreateRequestHelper().Mock();
            var createResponse = await _fixture.PostAsync<LoginResponse>(
                new CustomerUrl().Index,
                createRequest,
                HttpStatusCode.Created
            );
            var resquestBody = new LoginRequestHelper().Mock(createRequest);

            var response = await _fixture.PostAsync<LoginResponse>(
                new CustomerUrl().Login,
                resquestBody,
                HttpStatusCode.OK
            );

            _fixture.SetAuthHeader(response.Content.LoginToken);
            var detailResponse = await _fixture.GetAsync<CustomerResponse>(
                new CustomerUrl().Detail(response.Content.Customer.CustomerId),
                HttpStatusCode.OK
            );
            Assert.True(detailResponse.Content.CustomerId > 0);
            Assert.True(detailResponse.Content.Email == resquestBody.Email);
            Assert.True(detailResponse.Content.CustomerId > 0);
        }
    }
}
