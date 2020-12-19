using Ardalis.GuardClauses;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Models
{
    public abstract class BaseOrderStatusResponse : EnumerationResponse
    {
        protected BaseOrderStatusResponse()
        {
        }

        public string CssClass { set; get; }
    }

    public class OrderStatusResponse : BaseOrderStatusResponse
    {
        private OrderStatusResponse()
        {
        }
    }

    public class OrderStatusCssClassResponse
    {
        private OrderStatusCssClassResponse(string value)
        {
        }

        public string Value { get; set; }
    }
}
