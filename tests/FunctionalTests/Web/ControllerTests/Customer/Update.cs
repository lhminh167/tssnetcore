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

        [Fact, Trait("Priority", "2")]
        public async Task ReturnsOkResult()//done
        {
            var createResquestBody = new CustomerCreateRequestHelper().Mock();
            var createResponse = await _fixture.PostAsync<LoginResponse>(
                new CustomerUrl().Index,
                createResquestBody,
                HttpStatusCode.Created
            );
            _fixture.SetAuthHeader(createResponse.Content.LoginToken);
            var resquestBody = new CustomerResponseHelper().Mock(createResponse.Content.Customer.CustomerId);

            var response = await _fixture.PutAsync<CustomerResponse>(
                new CustomerUrl().Detail(createResponse.Content.Customer.CustomerId),
                resquestBody,
                HttpStatusCode.OK
            );

            var detailResponse = await _fixture.GetAsync<CustomerResponse>(
                new CustomerUrl().Detail(createResponse.Content.Customer.CustomerId),
                HttpStatusCode.OK
            );
            Assert.Equal(detailResponse.Content.Name, resquestBody.Name);
        }
    }
}
