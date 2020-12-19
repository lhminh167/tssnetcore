using System;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;

namespace Tss.Core.Entities
{
    public class Audit
    {
        public int AuditId { get; private set; }
        public int OrderId { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string Message { get; private set; }
        public int Code { get; private set; }

        public virtual Orders Order { get; private set; }

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
