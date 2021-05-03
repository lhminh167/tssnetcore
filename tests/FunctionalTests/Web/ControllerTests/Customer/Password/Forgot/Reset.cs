
using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Helpers;

namespace Tss.FunctionalTests.Web.ControllerTests.Customer.Password.Forgot
{
    [Collection("Startup collection")]
    public class Reset
    {
        private readonly WebFixture _fixture;

        public Reset(
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

            var forgotPasswordResquestBody = new CustomerPasswordForgotRequest()
            {
                Email = createRequest.Email
            };
            var forgotPasswordResponse = await _fixture.PutAsync<CustomerResponse>(
                new CustomerUrl().ForgotPassword(),
                forgotPasswordResquestBody,
                HttpStatusCode.OK
            );
            _fixture.SetAuthHeader(_fixture.TestCustomerLoginToken);
            var resetPasswordTokenResponse = await _fixture.GetAsync<CustomerPasswordResetTokenGetResponse>(
                new CustomerUrl().GetResetPasswordToken(createResponse.Content.Customer.CustomerId),
                HttpStatusCode.OK
            );
            _fixture.SetAuthHeader(null);
            var validatePasswordTokenResponse = await _fixture.GetAsync<CustomerResponse>(
                new CustomerUrl().ValidateResetPasswordToken(
                    createResponse.Content.Customer.CustomerId,
                    resetPasswordTokenResponse.Content.ResetPasswordToken
                ),
                HttpStatusCode.OK
            );
            var resetPasswordResquest = new CustomerPasswordResetRequest()
            {
                CustomerId = createResponse.Content.Customer.CustomerId,
                ResetPasswordToken = resetPasswordTokenResponse.Content.ResetPasswordToken,
                NewPassword = new RandomGeneratorHelper().RandomString(5)
            };
            var resetPasswordResponse = await _fixture.PutAsync<CustomerResponse>(
                new CustomerUrl().ResetForgottenPassword(resetPasswordResquest),
                resetPasswordResquest,
                HttpStatusCode.OK
            );

            var loginResquestBody = new LoginRequestHelper().Mock(createRequest);
            loginResquestBody.Password = resetPasswordResquest.NewPassword;
            var loginResponse = await _fixture.PostAsync<LoginResponse>(
                new CustomerUrl().Login,
                loginResquestBody,
                HttpStatusCode.OK
            );
        }
    }
}
