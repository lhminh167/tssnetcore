using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class PsFinalNotificationEmail : IEmail
    {
        private readonly AppSettings _settings;
        private readonly Orders _order;

        public PsFinalNotificationEmail(
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
            Subject = "Order Final Notification";
            PlainTextContent = null;
            HtmlContent = GetMailBody();
        }

        public string SenderEmailAddress {get; } 
        public string SenderName { get; }
        public string RecipientEmailAddress { get; }
        public string RecipientName { get; }
        public string Subject { get; }
        public string PlainTextContent { get; }
        public string HtmlContent { get; }

        private string GetMailBody()
        {
            string body = $@"Your order has now been dispatched! The following products have been shipped:
            {_order.GetOrderAsString(false)}

            Your order has been shipped to:
            {_order.GetCustomerAddressAsString()}

            Please check availability and confirm via
            LinkToAdmin

            Order reference number: {_order.OrderId}

            Thank you for shopping at T-Shirt Shop!
            ";

            return body;
        }
    }
}
