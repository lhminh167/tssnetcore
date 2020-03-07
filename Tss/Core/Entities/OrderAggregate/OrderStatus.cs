using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public abstract class BaseOrderStatus : Enumeration
    {
        protected BaseOrderStatus(int id, string message)
        : base(id, message)
        {
            // Guard.Against.OutOfRange(id, nameof(id), 1, 10);
        }
    }

    public class OrderStatus : BaseOrderStatus
    {
        public static OrderStatus OrderPlacedNotifyingCustomer = new OrderStatus(1, "Order placed, notifying customer");
        public static OrderStatus AwaitingConfirmationOfFunds = new OrderStatus(2, "Awaiting confirmation of funds");
        public static OrderStatus NotifyingSupplierStockCheck = new OrderStatus(3, "Notifying supplier-stock check");
        public static OrderStatus AwaitingStockConfirmation = new OrderStatus(4, "Awaiting stock confirmation");
        public static OrderStatus AwaitingCreditCardPayment = new OrderStatus(5, "Awaiting credit card payment");
        public static OrderStatus NotifyingSupplierShipping = new OrderStatus(6, "Notifying supplier-shipping");
        public static OrderStatus AwaitingShipmentConfirmation = new OrderStatus(7, "Awaiting shipment confirmation");
        public static OrderStatus SendingFinalNotification = new OrderStatus(8, "Sending final notification");
        public static OrderStatus OrderCompleted = new OrderStatus(9, "Order completed");
        public static OrderStatus OutOfStock = new OrderStatus(10, "Order canceled because some products are out of stock");

        public OrderStatus(int id, string message)
        : base(id, message)
        {
        }
    }
}
