using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;
using System;
using System.Net;

namespace Tss.Core.Entities
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
        public string PlainTextContent { get; private set; }
        public string HtmlContent { get; }

        private string GetMailBody()
        {
            Guard.Against.NullOrWhiteSpace(_settings.FrontendUrl, nameof(_settings.FrontendUrl));
            string resetUrl = new Uri(
                new Uri(_settings.FrontendUrl.TrimEnd('/') + "/"),
                $"user/{_customer.CustomerId}/reset-password/{Uri.EscapeDataString(_customer.ResetPasswordToken)}"
            ).AbsoluteUri;
            string customerName = WebUtility.HtmlEncode(_customer.Name);
            string encodedResetUrl = WebUtility.HtmlEncode(resetUrl);

            PlainTextContent = $"Hi {_customer.Name},\n\nPlease click the following link to reset your password:\n{resetUrl}\n\nIf you did not request this, please ignore this email and your password will remain unchanged.";
            return $"<p>Hi {customerName},</p><p>Please click the following link to reset your password:</p><p><a href=\"{encodedResetUrl}\">Reset your password</a></p><p>If you did not request this, please ignore this email and your password will remain unchanged.</p>";
        }
    }
}
