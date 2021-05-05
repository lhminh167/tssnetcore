using System;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Entities
{
    public class PsInitialNotificationEmail : IEmail
    {
        private readonly AppSettings _settings;
        private readonly Orders _order;

        public PsInitialNotificationEmail(
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
            Subject = "Order Initial Notification";
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
            string body = $@"Thank you for your order!
            

            The products that your ordered are as follows;
            
            {_order.GetOrderAsString(false)}


            Your order will be shipped to:

            {_order.GetCustomerAddressAsString()}


            Order reference number: {_order.OrderId}

            You will receive a confirmation e-mail when this order has been dispatched. Thank you for shopping at T-shirt Shop!
            ";

            return body;
        }
    }
}
