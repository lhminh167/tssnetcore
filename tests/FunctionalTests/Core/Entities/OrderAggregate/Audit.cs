using System;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;

namespace Tss.FunctionalTests.Core.Entities
{
    public class Audit
    {
        public int AuditId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }

        public virtual Orders Order { get; set; }

        public Audit(string message, int code, int orderId)
        {
            Guard.Against.NullOrWhiteSpace(message, nameof(message));
            Guard.Against.OutOfRange(code, nameof(code), 1, int.MaxValue);

            OrderId = orderId;
            Code = code;
            Message = message;
            CreatedOn = DateTime.Now;
        }
    }
}
