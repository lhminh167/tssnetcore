using System.Net;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class PaymentCreateResponse : Response
    {
        public PaymentCreateResponse(
            string paymentId,
            string paymentStatus,
            string paymentSecret,
            HttpStatusCode status,
            string title,
            string detail
        )
        : base(
            status,
            title,
            detail
        )
        {
            Guard.Against.NullOrWhiteSpace(paymentId, nameof(paymentId));
            Guard.Against.NullOrWhiteSpace(paymentStatus, nameof(paymentStatus));
            Guard.Against.NullOrWhiteSpace(paymentSecret, nameof(paymentSecret));

            PaymentId = paymentId;
            PaymentStatus = paymentStatus;
            PaymentSecret = paymentSecret;
        }

        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentSecret { get; set; }

    }
}
