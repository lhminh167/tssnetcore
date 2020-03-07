using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class PaymentCreateResponse
    {
        public PaymentCreateResponse(string paymentId, string paymentStatus, string paymentSecret)
        {
            Guard.Against.Null(paymentId, nameof(paymentId));
            Guard.Against.Null(paymentStatus, nameof(paymentStatus));
            Guard.Against.Null(paymentSecret, nameof(paymentSecret));

            PaymentId = paymentId;
            PaymentStatus = paymentStatus;
            PaymentSecret = paymentSecret;
        }

        public string PaymentId { get; private set; }
        public string PaymentStatus { get; private set; }
        public string PaymentSecret { get; private set; }

    }
}
