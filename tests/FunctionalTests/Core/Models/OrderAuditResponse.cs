using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
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