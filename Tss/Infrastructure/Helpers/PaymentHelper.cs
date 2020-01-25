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
                PaymentMethod = order.Reference,
                Currency = "usd",
                CaptureMethod = "manual",
                // Confirm = false,
                // PaymentMethodTypes = new List<string> { "card" },
                Metadata = new Dictionary<string, string>
                {
                    { "OrderId", order.OrderId.ToString() },
                },
                Amount = (long)(order.TotalAmount * 100),
            };
            PaymentIntent response = await _paymentIntentService.CreateAsync(options);
            return new PaymentCreateResponse(response.Id);

        }

        public async Task<PaymentCreateResponse> CompletePayment(string paymentId)
        {
            // PaymentIntentCaptureOptions options = new PaymentIntentCaptureOptions
            // {
            //     // AmountToCapture = 750,
            // };
            PaymentIntent confirmResponse = await _paymentIntentService.ConfirmAsync(paymentId);
            PaymentIntent captureResponse = await _paymentIntentService.CaptureAsync(paymentId);

            return new PaymentCreateResponse(captureResponse.ClientSecret);

        }



    }
}