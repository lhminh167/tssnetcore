using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;
using System.Net;

namespace Tss.Core.Entities
{
    public class PasswordChangeConfirmationEmail : IEmail
    {
        private readonly AppSettings _settings;
        private readonly Customer _customer;

        public PasswordChangeConfirmationEmail(
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
            Subject = "Your Password Has Been Changed";
            PlainTextContent = GetPlainTextBody();
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
            string customerEmail = WebUtility.HtmlEncode(_customer.Email);
            return $"<p>This is a confirmation that the password for your account <strong>{customerEmail}</strong> has just been changed.</p>";
        }

        private string GetPlainTextBody()
        {
            return $"This is a confirmation that the password for your account {_customer.Email} has just been changed.";
        }
    }
}
