using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public abstract class BaseOrderStatus : Enumeration
    {
        protected BaseOrderStatus(int id, string message)
        : base(id, message)
        {
            Guard.Against.OutOfRange(id, nameof(id), 0, 9);
        }
    }

    public class OrderStatus : BaseOrderStatus
    {
        public static OrderStatus OrderPlacedNotifyingCustomer = new OrderStatus(0, "Order placed, notifying customer");
        public static OrderStatus AwaitingConfirmationOfFunds = new OrderStatus(1, "Awaiting confirmation of funds");
        public static OrderStatus NotifyingSupplierStockCheck = new OrderStatus(2, "Notifying supplier-stock check");
        public static OrderStatus AwaitingStockConfirmation = new OrderStatus(3, "Awaiting stock confirmation");
        public static OrderStatus AwaitingCreditCardPayment = new OrderStatus(4, "Awaiting credit card payment");
        public static OrderStatus NotifyingSupplierShipping = new OrderStatus(5, "Notifying supplier-shipping");
        public static OrderStatus AwaitingShipmentConfirmation = new OrderStatus(6, "Awaiting shipment confirmation");
        public static OrderStatus SendingFinalNotification = new OrderStatus(7, "Sending final notification");
        public static OrderStatus OrderCompleted = new OrderStatus(8, "Order completed");
        public static OrderStatus OrderCanceled = new OrderStatus(9, "Order canceled");

        public OrderStatus(int id, string message)
        : base(id, message)
        {
        }
    }
}
