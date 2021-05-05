using System.Net;
using Ardalis.GuardClauses;

namespace Tss.FunctionalTests.Core.Models
{
    public class PaymentCreateResponse : Response
    {
        public PaymentCreateResponse()
        {
        }

        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentSecret { get; set; }

    }
}
