using System;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Entities
{
    public class PsCheckStockEmail : IEmail
    {
        private readonly AppSettings _settings;
        private readonly Orders _order;

        public PsCheckStockEmail(
            Orders order,
            AppSettings settings
        )
        {
            Guard.Against.Null(order, nameof(order));
            Guard.Against.Null(settings, nameof(settings));

            _settings = settings;
            _order = order;

            SenderEmailAddress = _settings.AdminEmail;
            SenderName = _settings.AdminName;
            RecipientEmailAddress = _settings.SupplierEmail;
            RecipientName = _settings.SupplierName;
            Subject = "Check Stock Notification";
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
            string body = $@"The following goods have been ordered:            
            {_order.GetOrderAsString(false)}

            Please check availability and confirm via
            LinkToAdmin

            Order reference number: {_order.OrderId}
            ";

            return body;
        }
    }
}
