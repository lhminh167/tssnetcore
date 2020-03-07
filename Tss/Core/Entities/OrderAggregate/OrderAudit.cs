using System;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    // public class ImprovedAudit
    // {

    //     public string AuditId { get; private set; }
    //     public string CustomerId { get; private set; }
    //     public string CorrelationId { get; private set; }
    //     public string PreviousValue { get; private set; }
    //     public string CurrentValue { get; private set; }
    //     public string Message { get; private set; }
    //     public int Code { get; private set; }
    //     public string Data { get; private set; }
    // }

    public abstract class BaseOrderAudit : Enumeration
    {
        public BaseOrderAudit(string message, int id)
        : base(id, message)
        {
            // Guard.Against.OutOfRange(id, nameof(id), 10000, 20702);
        }
    }

    public class OrderAudit : BaseOrderAudit
    {
        public static OrderAudit OrderProcessorStarted = new OrderAudit(
            "Order Processor started.",
            10000
        );

        public static OrderAudit OrderProcessorFinished = new OrderAudit(
            "Order Processor finished.",
            10001
        );

        public static OrderAudit PsInitialNotificationStarted = new OrderAudit(
            "PsInitialNotification started.",
            20000
        );

        public static OrderAudit PsInitialNotificationFinished = new OrderAudit(
            "PsInitialNotification finished.",
            20001
        );

        public static OrderAudit PsInitialNotificationEmailSent = new OrderAudit(
            "Notification email sent to customer.",
            20002
        );

        public static OrderAudit PsCheckFundsStarted = new OrderAudit(
            "PsCheckFunds started.",
            20100
        );

        public static OrderAudit PsCheckFundsFinished = new OrderAudit(
            "PsCheckFunds finished.",
            20101
        );

        public static OrderAudit PsCheckFundsFundsAvailable = new OrderAudit(
            "Funds available for purchase.",
            20102
        );

        public static OrderAudit PsCheckStockStarted = new OrderAudit(
            "PsCheckStock started.",
            20200
        );

        public static OrderAudit PsCheckStockFinished = new OrderAudit(
            "PsCheckStock finished.",
            20201
        );

        public static OrderAudit PsCheckStockEmailSentToSupplier = new OrderAudit(
            "Notification email sent to supplier.",
            20202
        );

        public static OrderAudit PsStockOkStarted = new OrderAudit(
            "PsStockOk started.",
            20300
        );

        public static OrderAudit PsStockOkFinished = new OrderAudit(
            "PsStockOk finished.",
            20301
        );

        public static OrderAudit PsStockOkStockConfirmedBySupplier = new OrderAudit(
            "Stock confirmed by supplier.",
            20302
        );

        public static OrderAudit PsTakePaymentStarted = new OrderAudit(
            "PsTakePayment started.",
            20400
        );

        public static OrderAudit PsTakePaymentFinished = new OrderAudit(
            "PsTakePayment finished.",
            20401
        );

        public static OrderAudit PsTakePaymentFundsDeducted = new OrderAudit(
            "Funds deducted from customer credit card account.",
            20402
        );

        public static OrderAudit PsShipGoodsStarted = new OrderAudit(
            "PsShipGoods started.",
            20500
        );

        public static OrderAudit PsShipGoodsFinished = new OrderAudit(
            "PsShipGoods finished.",
            20501
        );

        public static OrderAudit PsShipGoodsEmailSentToSupplier = new OrderAudit(
            "Ship goods e-mail sent to supplier.",
            20502
        );

        public static OrderAudit PsShipOkStarted = new OrderAudit(
            "PsShipOk started.",
            20600
        );

        public static OrderAudit PsShipOkFinished = new OrderAudit(
            "PsShipOk finished.",
            20601
        );

        public static OrderAudit PsShipOkOrderDispatched = new OrderAudit(
            "Order dispatched by supplier.",
            20602
        );

        public static OrderAudit PsFinalNotificationStarted = new OrderAudit(
            "PsFinalNotification started.",
            20700
        );

        public static OrderAudit PsFinalNotificationFinished = new OrderAudit(
            "PsFinalNotification finished.",
            20701
        );

        public static OrderAudit PsFinalNotificationEmailSent = new OrderAudit(
            "Dispatch email sent to customer.",
            20702
        );

        public static OrderAudit PsStockNotOkStarted = new OrderAudit(
            "PsStockNotOk started.",
            20800
        );

        public static OrderAudit PsStockNotOkFinished = new OrderAudit(
            "PsStockNotOk finished.",
            20801
        );

        public static OrderAudit PsStockNotOkEmailSent = new OrderAudit(
            "Out-of-stock email sent to customer.",
            20802
        );

        OrderAudit(string message, int id)
        : base(message, id)
        {
        }
    }
}