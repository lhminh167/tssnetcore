using System.Threading.Tasks;
using Tss.Core.Interfaces;
using Microsoft.Extensions.Options;
using Tss.Core.Models;
using Stripe;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using Tss.Core.Entities;
using System;

namespace Microsoft.eShopWeb.Infrastructure.Helpers
{
    public class PaymentHelper : IPaymentHelper
    {
        private readonly AppSettings _settings;
        private readonly PaymentIntentService _paymentIntentService;
        private readonly PaymentMethodService _paymentMethodService;

        public PaymentHelper(IOptions<AppSettings> settings)
        {
            Guard.Against.Null(settings, nameof(settings));

            _settings = settings.Value;
            _paymentIntentService = new PaymentIntentService();
            _paymentMethodService = new PaymentMethodService();

            StripeConfiguration.ApiKey = _settings.StripeSecretKey;
        }

        public async Task<PaymentCreateResponse> CreatePayment(Orders order)
        {
            var options = new PaymentIntentCreateOptions
            {
                PaymentMethod = order.Customer.CreditCard,
                Currency = "usd",
                CaptureMethod = "manual",
                // Confirm = false,
                // PaymentMethodTypes = new List<string> { "card" },
                // SavePaymentMethod = true,
                Metadata = new Dictionary<string, string>
                {
                    { "OrderId", order.OrderId.ToString() },
                },
                Amount = (long)(order.TotalAmount * 100),
            };
            PaymentIntent response = await _paymentIntentService.CreateAsync(options);
            PaymentIntent confirmResponse = await _paymentIntentService.ConfirmAsync(response.Id);
            return new PaymentCreateResponse(confirmResponse.Id, confirmResponse.Status, confirmResponse.ClientSecret);

        }

        public async Task<PaymentCreateResponse> CompletePayment(string paymentId)
        {
            PaymentIntent response = await _paymentIntentService.CaptureAsync(paymentId);

            return new PaymentCreateResponse(response.Id, response.Status, response.ClientSecret);
        }


        public async Task<PaymentCreateResponse> CancelPayment(string paymentId)
        {
            PaymentIntent response = await _paymentIntentService.CancelAsync(paymentId);

            return new PaymentCreateResponse(response.Id, response.Status, response.ClientSecret);
        }

    }
}