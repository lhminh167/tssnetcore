using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class OrderCreateResponse
    {
        public OrderCreateResponse(Orders order, PaymentCreateResponse response)
        {
            Guard.Against.Null(order, nameof(order));
            Guard.Against.Null(response, nameof(response));

            Order = order;
            PaymentId = response.PaymentId;
            PaymentStatus = response.PaymentStatus;
            PaymentSecret = response.PaymentSecret;
        }

        public Orders Order { get; private set; }
        public string PaymentId { get; private set; }
        public string PaymentStatus { get; private set; }
        public string PaymentSecret { get; private set; }
    }
}
