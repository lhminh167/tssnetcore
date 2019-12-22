using System;
using System.Threading.Tasks;
using Tss.Core.Interfaces;

namespace Tss.Core.Entities
{
    public partial class Orders
    {
        public async Task PsInitialNotification(IEmailHelper emailHelper)
        {
            if (Status != OrderStatus.OrderPlacedNotifyingCustomer.Id) StatusChangeException(OrderStatus.AwaitingConfirmationOfFunds);
            CreateAudit(OrderAudit.OrderProcessorStarted);
            await emailHelper.SendOrderInitialNotification(this);
            SetOrderStatus(OrderStatus.AwaitingConfirmationOfFunds);
            CreateAudit(OrderAudit.OrderProcessorFinished);
            await PsCheckFunds(emailHelper);
        }

        private async Task PsCheckFunds(IEmailHelper emailHelper)
        {
            if (Status != OrderStatus.AwaitingConfirmationOfFunds.Id) StatusChangeException(OrderStatus.NotifyingSupplierStockCheck);
            CreateAudit(OrderAudit.PsCheckFundsStarted);
            SetCreditCardInfo("DummyAuthCode", "DummyReference");
            CreateAudit(OrderAudit.PsCheckFundsFundsAvailable);
            SetOrderStatus(OrderStatus.NotifyingSupplierStockCheck);
            CreateAudit(OrderAudit.PsCheckFundsFinished);
            await PsCheckStock(emailHelper);
        }

        private async Task PsCheckStock(IEmailHelper emailHelper)
        {
            if (Status != OrderStatus.NotifyingSupplierStockCheck.Id) StatusChangeException(OrderStatus.AwaitingStockConfirmation);
            CreateAudit(OrderAudit.PsCheckStockStarted);
            await emailHelper.SendCheckStockNotification(this);
            CreateAudit(OrderAudit.PsCheckStockEmailSentToSupplier);
            SetOrderStatus(OrderStatus.AwaitingStockConfirmation);
            CreateAudit(OrderAudit.PsCheckStockFinished);
        }

        public async Task PsStockOk(IEmailHelper emailHelper, IPaymentHelper paymentHelper)
        {
            if (Status != OrderStatus.AwaitingStockConfirmation.Id) StatusChangeException(OrderStatus.AwaitingCreditCardPayment);
            CreateAudit(OrderAudit.PsStockOkStarted);
            CreateAudit(OrderAudit.PsStockOkStockConfirmedBySupplier);
            SetOrderStatus(OrderStatus.AwaitingCreditCardPayment);
            CreateAudit(OrderAudit.PsStockOkFinished);
            await PsTakePayment(emailHelper, paymentHelper);
        }

        private async Task PsTakePayment(IEmailHelper emailHelper, IPaymentHelper paymentHelper)
        {
            if (Status != OrderStatus.AwaitingCreditCardPayment.Id) StatusChangeException(OrderStatus.NotifyingSupplierShipping);
            CreateAudit(OrderAudit.PsTakePaymentStarted);
            await paymentHelper.TakePaymentAsync(this);
            CreateAudit(OrderAudit.PsTakePaymentFundsDeducted);
            SetOrderStatus(OrderStatus.NotifyingSupplierShipping);
            CreateAudit(OrderAudit.PsTakePaymentFinished);
            await PsShipGoods(emailHelper);
        }

        private async Task PsShipGoods(IEmailHelper emailHelper)
        {
            if (Status != OrderStatus.NotifyingSupplierShipping.Id) StatusChangeException(OrderStatus.AwaitingShipmentConfirmation);
            CreateAudit(OrderAudit.PsShipGoodsStarted);
            await emailHelper.SendShipGoodsNotification(this);
            CreateAudit(OrderAudit.PsShipGoodsEmailSentToSupplier);
            SetOrderStatus(OrderStatus.AwaitingShipmentConfirmation);
            CreateAudit(OrderAudit.PsShipGoodsFinished);
        }

        public async Task PsShipOk(IEmailHelper emailHelper)
        {
            if (Status != OrderStatus.AwaitingShipmentConfirmation.Id) StatusChangeException(OrderStatus.SendingFinalNotification);
            CreateAudit(OrderAudit.PsShipOkStarted);
            SetDateShipped();
            CreateAudit(OrderAudit.PsShipOkOrderDispatched);
            SetOrderStatus(OrderStatus.SendingFinalNotification);
            CreateAudit(OrderAudit.PsShipOkFinished);
            await PsFinalNotification(emailHelper);
        }

        private async Task PsFinalNotification(IEmailHelper emailHelper)
        {
            if (Status != OrderStatus.SendingFinalNotification.Id) StatusChangeException(OrderStatus.OrderCompleted);
            CreateAudit(OrderAudit.PsFinalNotificationStarted);
            await emailHelper.SendOrderFinalNotification(this);
            CreateAudit(OrderAudit.PsFinalNotificationEmailSent);
            SetOrderStatus(OrderStatus.OrderCompleted);
            CreateAudit(OrderAudit.PsFinalNotificationFinished);
        }

        // public async Task SetOrderCanceledStatus(IEmailHelper emailHelper, IPaymentHelper paymentHelper)
        // {
        //     if (Status != OrderStatus.AwaitingConfirmationOfFunds.Id) StatusChangeException(OrderStatus.OrderCanceled);
        //     CreateAudit(OrderAudit.PsCheckFundsStarted);
        //     SetCreditCardInfo("DummyAuthCode", "DummyReference");
        //     CreateAudit(OrderAudit.PsCheckFundsFundsAvailable);
        //     SetOrderStatus(OrderStatus.OrderCanceled);
        //     CreateAudit(OrderAudit.PsCheckFundsFinished);
        // }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new Exception($"It is not possible to change the order status from {OrderStatus.Message} to {orderStatusToChange.Message}.");
        }
    }
}
