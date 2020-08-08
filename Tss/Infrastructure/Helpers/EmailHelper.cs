using System.Threading.Tasks;
using Tss.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using Tss.Core.Models;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Microsoft.eShopWeb.Infrastructure.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly AppSettings _settings;
        private readonly ISendGridClient _client;

        public EmailHelper(
            IOptions<AppSettings> settings
        )
        {
            Guard.Against.Null(settings, nameof(settings));
            Guard.Against.Null(settings.Value, nameof(settings.Value));

            _settings = settings.Value;
            _client = new SendGridClient(_settings.SendgridApiKey);
        }

        private async Task SendEmailAsync(IEmail email)
        {
            EmailAddress from = new EmailAddress(email.SenderEmailAddress, email.SenderName);
            EmailAddress to = new EmailAddress(email.RecipientEmailAddress, email.RecipientName);
            var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.PlainTextContent, email.HtmlContent);
            var res = await _client.SendEmailAsync(msg);

            return;
        }

        public async Task SendOrderInitialNotification(Orders order)
        {
            await SendEmailAsync(new PsInitialNotificationEmail(order, _settings));
        }

        public async Task SendCheckStockNotification(Orders order)
        {
            await SendEmailAsync(new PsCheckStockEmail(order, _settings));
        }

        public async Task SendShipGoodsNotification(Orders order)
        {
            await SendEmailAsync(new PsShipGoodsEmail(order, _settings));
        }

        public async Task SendOrderFinalNotification(Orders order)
        {
            await SendEmailAsync(new PsFinalNotificationEmail(order, _settings));
        }

        public async Task SendStockNotOkNotification(Orders order)
        {
            await SendEmailAsync(new PsStockNotOkNotificationEmail(order, _settings));
        }

        public async Task SendInsufficientFundNotification(Orders order)
        {
            await SendEmailAsync(new PsInsufficientFundNotificationEmail(order, _settings));
        }

        public async Task SendResetPasswordEmail(Customer customer)
        {
            await SendEmailAsync(new ResetPasswordEmail(customer, _settings));
        }

        public async Task SendPasswordChangeConfirmationEmail(Customer customer)
        {
            await SendEmailAsync(new ResetPasswordEmail(customer, _settings));
        }
    }
}