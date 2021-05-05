namespace Tss.FunctionalTests.Core.Models
{
    public abstract class BaseOrderStatusResponse : EnumerationResponse
    {
        public BaseOrderStatusResponse()
        {
        }

        public string CssClass { set; get; }
    }

    public class OrderStatusResponse : BaseOrderStatusResponse
    {
        public OrderStatusResponse()
        {
        }
    }

    public class OrderStatusCssClassResponse
    {
        public OrderStatusCssClassResponse(string value)
        {
        }

        public string Value { get; set; }
    }
}
