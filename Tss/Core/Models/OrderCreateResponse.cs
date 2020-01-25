using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class OrderCreateResponse
    {
        public OrderCreateResponse(Orders order, string paymentId)
        {
            Guard.Against.Null(order, nameof(order));
            Guard.Against.Null(paymentId, nameof(paymentId));

            Order = order;
            PaymentId = paymentId;
        }

        public Orders Order { get; private set; }
        public string PaymentId { get; private set; }
    }
}
