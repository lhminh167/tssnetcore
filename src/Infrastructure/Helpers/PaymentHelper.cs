using System.Threading.Tasks;
using Tss.Core.Interfaces;
using Microsoft.Extensions.Options;
using Tss.Core.Models;
using Stripe;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using Tss.Core.Entities;
using System;
using Newtonsoft.Json;

namespace Tss.Infrastructure.Helpers
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

        public async Task<PaymentCreateResponse> CreatePayment(
            string paymentMethodId,
            string currency,
            double amount
        )
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    PaymentMethod = paymentMethodId,
                    Currency = currency,
                    CaptureMethod = "manual",
                    Amount = (long)(amount * 100),
                    Metadata = new Dictionary<string, string>
                    {
                        { "integration_check", "accept_a_payment" },
                    },
                };
                PaymentIntent response = await _paymentIntentService.CreateAsync(options);
                PaymentIntent confirmResponse = await _paymentIntentService.ConfirmAsync(response.Id);
                return new PaymentCreateResponse(
                    confirmResponse.Id,
                    confirmResponse.Status,
                    confirmResponse.ClientSecret,
                    confirmResponse.StripeResponse.StatusCode,
                    "Payment was created successfully",
                    "Payment was created successfully"
                );
            }
            catch (StripeException e)
            {
                return HandleExceptions(e);
            }
        }

        public async Task<PaymentCreateResponse> UpdatePayment(string paymentId, Dictionary<string, string> metadata)
        {
            try
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Metadata = metadata
                };

                PaymentIntent response = await _paymentIntentService.UpdateAsync(paymentId, options);

                return new PaymentCreateResponse(
                    response.Id,
                    response.Status,
                    response.ClientSecret,
                    response.StripeResponse.StatusCode,
                    "Payment was updated successfully",
                    "Payment was updated successfully"
                );
            }
            catch (StripeException e)
            {
                return HandleExceptions(e);
            }
        }

        public async Task<PaymentCreateResponse> CompletePayment(string paymentId)
        {
            try
            {
                PaymentIntent response = await _paymentIntentService.CaptureAsync(paymentId);

                return new PaymentCreateResponse(
                    response.Id,
                    response.Status,
                    response.ClientSecret,
                    response.StripeResponse.StatusCode,
                    "Payment was completed successfully",
                    "Payment was completed successfully"
                );
            }
            catch (StripeException e)
            {
                return HandleExceptions(e);
            }
        }

        public async Task<PaymentCreateResponse> CancelPayment(string paymentId)
        {
            try
            {

                PaymentIntent response = await _paymentIntentService.CancelAsync(paymentId);

                return new PaymentCreateResponse(
                    response.Id,
                    response.Status,
                    response.ClientSecret,
                    response.StripeResponse.StatusCode,
                    "Payment was cancelled successfully",
                    "Payment was cancelled successfully"
                );

            }
            catch (StripeException e)
            {
                return HandleExceptions(e);
            }
        }

        private PaymentCreateResponse HandleExceptions(StripeException e)
        {
            Console.WriteLine("Code: " + e.StripeError.Code);
            Console.WriteLine("Message: " + e.StripeError.Message);
            return new PaymentCreateResponse(
                e.StripeError.PaymentIntent.Id,
                e.StripeError.PaymentIntent.Status,
                e.StripeError.PaymentIntent.ClientSecret,
                e.StripeError.StripeResponse.StatusCode,
                e.StripeError.Message,
                e.StripeError.Message
            );
        }
    }
}