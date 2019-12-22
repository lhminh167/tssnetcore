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
        private readonly PaymentIntentService _paymentIntentService = new PaymentIntentService();

        public PaymentHelper(IOptions<AppSettings> settings)
        {
            Guard.Against.Null(settings, nameof(settings));

            _settings = settings.Value;
            _paymentIntentService = new PaymentIntentService();

            StripeConfiguration.ApiKey = _settings.StripeSecretKey;
        }

        public async Task TakePaymentAsync(Orders order)
        {
            try
            {
                await HandlePaymentAsync(order);
            }
            catch (StripeException exception)
            {
                HandleException(exception);
            }
        }

        private async Task HandlePaymentAsync(Orders order)
        {
            PaymentIntentCreateOptions options = new PaymentIntentCreateOptions
            {
                Amount = (long)(order.TotalAmount * 100),
                Currency = "usd",
                PaymentMethodTypes = new List<string> {
                    "card"
                },
                Metadata = new Dictionary<string, string>
                {
                    { "OrderId", order.OrderId.ToString() },
                },
                // Confirm = true,
                // OffSession = true,

            };

            var response = await _paymentIntentService.CreateAsync(options);

            HandleResponse(response);
        }

        private void HandleResponse(PaymentIntent response)
        {
            switch (response.Status)
            {
                case "requires_payment_method":
                    break;
                case "requires_confirmation":
                    break;
                case "requires_action":
                    break;
                case "processing":
                    break;
                case "requires_capture":
                    break;
                case "canceled":
                    break;
                case "succeeded":
                    break;
                default:
                    // Unknown Error Type
                    break;
            }
        }

        private void HandleException(StripeException exception)
        {
            switch (exception.StripeError.ErrorType)
            {
                case "card_error":
                    Console.WriteLine("Code: " + exception.StripeError.Code);
                    Console.WriteLine("Message: " + exception.StripeError.Message);
                    break;
                case "api_connection_error":
                    break;
                case "api_error":
                    break;
                case "authentication_error":
                    break;
                case "invalid_request_error":
                    break;
                case "rate_limit_error":
                    break;
                case "validation_error":
                    break;
                default:
                    // Unknown Error Type
                    break;
            }
        }
    }
}