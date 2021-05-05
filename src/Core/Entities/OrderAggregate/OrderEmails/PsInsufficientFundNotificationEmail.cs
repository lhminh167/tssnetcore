using System;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class PsInsufficientFundNotificationEmail : IEmail
    {
        private readonly AppSettings _settings;
        private readonly Orders _order;

        public PsInsufficientFundNotificationEmail(
            Orders order,
            AppSettings settings
        )
        {
            Guard.Against.Null(order, nameof(order));
            Guard.Against.Null(settings, nameof(settings));

            _settings = settings;
            _order = order;

            SenderEmailAddress = _settings.CustomerServiceEmail;
            SenderName = _settings.CustomerServiceEmail;
            RecipientEmailAddress = _order.Customer.Email;
            RecipientName = _order.Customer.Name;
            Subject = "Insufficient Fund Notification";
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
            string body = $@"Dear customer,
            
            Your order was cancelled because your credit card provides insufficient fund.
            
            The ordered items were put back into your shopping cart. You could adjust the quantity of products, or use another credit, then checkout once again.

            Order reference number: {_order.OrderId}

            Thank you for shopping at T-shirt Shop!
            ";

            return body;
        }
    }
}
