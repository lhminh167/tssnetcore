using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class PaymentCreateResponse
    {
        public PaymentCreateResponse(string paymentId)
        {
            Guard.Against.Null(paymentId, nameof(paymentId));

            PaymentId = paymentId;
        }

        public string PaymentId { get; private set; }
    }
}
