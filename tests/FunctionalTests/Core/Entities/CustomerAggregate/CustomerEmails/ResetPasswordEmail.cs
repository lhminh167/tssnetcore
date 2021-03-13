using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class ResetPasswordEmail : IEmail
    {
        private readonly AppSettings _settings;
        private readonly Customer _customer;

        public ResetPasswordEmail(
            Customer customer,
            AppSettings settings
        )
        {
            Guard.Against.Null(customer, nameof(customer));
            Guard.Against.Null(settings, nameof(settings));

            _settings = settings;
            _customer = customer;

            SenderEmailAddress = _settings.CustomerServiceEmail;
            SenderName = _settings.CustomerServiceEmail;
            RecipientEmailAddress = _customer.Email;
            RecipientName = _customer.Name;
            Subject = "Password Change Request";
            PlainTextContent = null;
            HtmlContent = GetMailBody();
        }

        public string SenderEmailAddress { get; }
        public string SenderName { get; }
        public string RecipientEmailAddress { get; }
        public string RecipientName { get; }
        public string Subject { get; }
        public string PlainTextContent { get; }
        public string HtmlContent { get; }

        private string GetMailBody()
        {
            string body = $@"
            Hi ${_customer.Name},

            Please click on the following link ${_customer.ResetPasswordToken} to reset your password.
            If you did not request this, please ignore this email and your password will remain unchanged.            
            ";

            return body;
        }
    }
}
