using System;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class PsShipGoodsEmail : IEmail
    {
        private readonly AppSettings _settings;
        private readonly Orders _order;

        public PsShipGoodsEmail(
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
            Subject = "Ship Goods Notification";
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
            string body = $@"Payment has been received for the following goods:
            {_order.GetOrderAsString(false)}

            Please ship to:
            {_order.GetCustomerAddressAsString()}

            When goods have been shipped, please confirm via:
            LinkToAdmin

            Order reference number: {_order.OrderId}
            ";

            return body;
        }
    }
}
