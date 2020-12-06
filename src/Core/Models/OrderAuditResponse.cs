using Tss.Core.Models;

namespace Tss.Core.Models
{
    public abstract class BaseOrderAuditResponse : EnumerationResponse
    {
        public BaseOrderAuditResponse()
        {
        }
    }

    public class OrderAuditResponse : BaseOrderAuditResponse
    {
        OrderAuditResponse()
        {
        }
    }
}