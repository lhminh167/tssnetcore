using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public abstract class BaseOrderStatus : Enumeration
    {
        protected BaseOrderStatus(int id, string message, OrderStatusCssClass cssClass)
        : base(id, message)
        {
            Guard.Against.Null(cssClass, nameof(cssClass));

            CssClass = cssClass.Value;
        }

        public string CssClass { private set; get; }
    }

    public class OrderStatus : BaseOrderStatus
    {
        public static OrderStatus OrderPlacedNotifyingCustomer = new OrderStatus(1, "Order placed, notifying customer", OrderStatusCssClass.Warning);
        public static OrderStatus AwaitingConfirmationOfFunds = new OrderStatus(2, "Awaiting confirmation of funds", OrderStatusCssClass.Warning);
        public static OrderStatus NotifyingSupplierStockCheck = new OrderStatus(3, "Notifying supplier-stock check", OrderStatusCssClass.Warning);
        public static OrderStatus AwaitingStockConfirmation = new OrderStatus(4, "Awaiting stock confirmation", OrderStatusCssClass.Warning);
        public static OrderStatus AwaitingCreditCardPayment = new OrderStatus(5, "Awaiting credit card payment", OrderStatusCssClass.Warning);
        public static OrderStatus NotifyingSupplierShipping = new OrderStatus(6, "Notifying supplier-shipping", OrderStatusCssClass.Warning);
        public static OrderStatus AwaitingShipmentConfirmation = new OrderStatus(7, "Awaiting shipment confirmation", OrderStatusCssClass.Warning);
        public static OrderStatus SendingFinalNotification = new OrderStatus(8, "Sending final notification", OrderStatusCssClass.Warning);
        public static OrderStatus OrderCompleted = new OrderStatus(9, "Order completed", OrderStatusCssClass.Success);
        public static OrderStatus OutOfStock = new OrderStatus(10, "Order canceled because some products are out of stock", OrderStatusCssClass.Danger);

        private OrderStatus(int id, string message, OrderStatusCssClass cssClass)
        : base(id, message, cssClass)
        {
        }
    }

    public class OrderStatusCssClass
    {
        private OrderStatusCssClass(string value) { Value = value; }

        public string Value { get; private set; }

        public static OrderStatusCssClass Success { get { return new OrderStatusCssClass("textSuccess"); } }
        public static OrderStatusCssClass Warning { get { return new OrderStatusCssClass("textWarning"); } }
        public static OrderStatusCssClass Danger { get { return new OrderStatusCssClass("textDanger"); } }
    }
}
