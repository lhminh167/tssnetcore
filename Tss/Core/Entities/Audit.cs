using System;
using Tss.Core.Interfaces;

namespace Tss.Core.Entities
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
            OrderId = orderId;
            CreatedOn = DateTime.Now;
            Code = code;
            Message = message;
        }
    }
}
