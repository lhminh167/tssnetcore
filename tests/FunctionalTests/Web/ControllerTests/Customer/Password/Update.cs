using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Helpers;

namespace Tss.FunctionalTests.Web.ControllerTests.Customer.Password
{
    [Collection("Startup collection")]
    public class Update
    {
        private readonly WebFixture _fixture;

        public Update(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "3")]
        public async Task ReturnsOkResult()//done
        {
            var createRequest = new CustomerCreateRequestHelper().Mock();
            var createResponse = await _fixture.PostAsync<LoginResponse>(
                new CustomerUrl().Index,
                createRequest,
                HttpStatusCode.Created
            );
            _fixture.SetAuthHeader(createResponse.Content.LoginToken);
            var resquestBody = new CustomerPasswordUpdateRequestHelper().Mock(
                createRequest,
                createResponse.Content.Customer.CustomerId
            );

            var response = await _fixture.PutAsync<CustomerResponse>(
                new CustomerUrl().UpdatePassword(createResponse.Content.Customer.CustomerId),
                resquestBody,
                HttpStatusCode.OK
            );

            var loginResquestBody = new LoginRequestHelper().Mock(createRequest);
            loginResquestBody.Password = resquestBody.NewPassword;
            var loginResponse = await _fixture.PostAsync<LoginResponse>(
                new CustomerUrl().Login,
                loginResquestBody,
                HttpStatusCode.OK
            );
            Assert.True(loginResponse.Content.Customer.CustomerId > 0);
            Assert.True(loginResponse.Content.Customer.Email == createRequest.Email);
        }
    }
}
