using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.Urls
{
    public class CustomerUrl
    {
        private readonly string _baseUrl = "/api/Customer/";
        public string Index => _baseUrl;
        public string Login => _baseUrl + "Login";
        public string Detail(int id) => _baseUrl + id;
        public string Update(int id) => $"{_baseUrl}{id}";
        public string UpdatePassword(int id) => $"{_baseUrl}{id}/Password";
        public string ResetForgottenPassword(CustomerPasswordResetRequest input)
            => Detail(input.CustomerId) + $"/ResetPassword/{input.ResetPasswordToken}";
        public string GetResetPasswordToken(int customerId)
            => Detail(customerId) + "/ResetPassword";
        public string ValidateResetPasswordToken(int customerId, string resetPasswordToken)
            => Detail(customerId) + $"/ResetPassword/{resetPasswordToken}";
        public string ForgotPassword() => $"{_baseUrl}ForgotPassword";
    }
}
