using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IPaymentHelper
    {
        Task<PaymentCreateResponse> CreatePayment(
            string paymentMethodId,
            string currency,
            double amount
        );
        Task<PaymentCreateResponse> UpdatePayment(string paymentId, Dictionary<string, string> metadata);
        Task<PaymentCreateResponse> CompletePayment(string paymentId);
        Task<PaymentCreateResponse> CancelPayment(string paymentId);

    }
}