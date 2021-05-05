using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class OrderCreateResponse
    {
        public OrderResponse Order { get; set; }
        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentSecret { get; set; }
    }
}
