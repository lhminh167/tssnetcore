using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public partial class Orders
    {
        public async Task<OrderCreateResponse> PsInitialNotification(IEmailHelper emailHelper, IPaymentHelper paymentHelper, string paymentMethodId)
        {
            if (Status != OrderStatus.OrderPlacedNotifyingCustomer.Id) StatusChangeException(OrderStatus.AwaitingConfirmationOfFunds);
            CreateAudit(OrderAudit.OrderProcessorStarted);
            await emailHelper.SendOrderInitialNotification(this);
            SetOrderStatus(OrderStatus.AwaitingConfirmationOfFunds);
            CreateAudit(OrderAudit.OrderProcessorFinished);
            return await PsCheckFunds(emailHelper, paymentHelper, paymentMethodId);
        }

        private async Task<OrderCreateResponse> PsCheckFunds(IEmailHelper emailHelper, IPaymentHelper paymentHelper, string paymentMethodId)
        {
            if (Status != OrderStatus.AwaitingConfirmationOfFunds.Id) StatusChangeException(OrderStatus.NotifyingSupplierStockCheck);
            CreateAudit(OrderAudit.PsCheckFundsStarted);
            Reference = paymentMethodId;
            PaymentCreateResponse response = await paymentHelper.CreatePayment(this);
            AuthCode = response.PaymentId;
            CreateAudit(OrderAudit.PsCheckFundsFundsAvailable);
            SetOrderStatus(OrderStatus.NotifyingSupplierStockCheck);
            CreateAudit(OrderAudit.PsCheckFundsFinished);
            await PsCheckStock(emailHelper, paymentHelper);
            return new OrderCreateResponse(this, response.PaymentId);
        }

        private async Task PsCheckStock(IEmailHelper emailHelper, IPaymentHelper paymentHelper)
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
            await paymentHelper.CompletePayment(this.AuthCode);
            CreateAudit(OrderAudit.PsTakePaymentFundsDeducted);
            SetOrderStatus(OrderStatus.NotifyingSupplierShipping);
            CreateAudit(OrderAudit.PsTakePaymentFinished);
            await PsShipGoods(emailHelper, paymentHelper);
        }

        private async Task PsShipGoods(IEmailHelper emailHelper, IPaymentHelper paymentHelper)
        {
            if (Status != OrderStatus.NotifyingSupplierShipping.Id) StatusChangeException(OrderStatus.AwaitingShipmentConfirmation);
            CreateAudit(OrderAudit.PsShipGoodsStarted);
            await emailHelper.SendShipGoodsNotification(this);
            CreateAudit(OrderAudit.PsShipGoodsEmailSentToSupplier);
            SetOrderStatus(OrderStatus.AwaitingShipmentConfirmation);
            CreateAudit(OrderAudit.PsShipGoodsFinished);
        }

        public async Task PsShipOk(IEmailHelper emailHelper, IPaymentHelper paymentHelper)
        {
            if (Status != OrderStatus.AwaitingShipmentConfirmation.Id) StatusChangeException(OrderStatus.SendingFinalNotification);
            CreateAudit(OrderAudit.PsShipOkStarted);
            SetDateShipped();
            CreateAudit(OrderAudit.PsShipOkOrderDispatched);
            SetOrderStatus(OrderStatus.SendingFinalNotification);
            CreateAudit(OrderAudit.PsShipOkFinished);
            await PsFinalNotification(emailHelper, paymentHelper);
        }

        private async Task PsFinalNotification(IEmailHelper emailHelper, IPaymentHelper paymentHelper)
        {
            if (Status != OrderStatus.SendingFinalNotification.Id) StatusChangeException(OrderStatus.OrderCompleted);
            CreateAudit(OrderAudit.PsFinalNotificationStarted);
            await emailHelper.SendOrderFinalNotification(this);
            CreateAudit(OrderAudit.PsFinalNotificationEmailSent);
            SetOrderStatus(OrderStatus.OrderCompleted);
            CreateAudit(OrderAudit.PsFinalNotificationFinished);
        }

        // public async Task SetOrderCanceledStatus(
        //     IEmailHelper emailHelper, 
        //     IPaymentHelper paymentHelper,
        //     OrderAudit sectionEndingAudit,
        //     OrderAudit cancellationAudit
        // )
        // {
        // Guard.Against.Null(emailHelper, nameof(emailHelper), paymentHelper);
        // Guard.Against.Null(paymentHelper, nameof(paymentHelper));

        //     CreateAudit(sectionEndingAudit);
        //     CreateAudit(OrderAudit.PsCheckFundsStarted);
        //     // await emailHelper.SendOrderCancellationNotification(this);
        //     CreateAudit(OrderAudit.PsCheckFundsFundsAvailable);
        //     // SetOrderStatus(cancellationAudit);
        //     // CreateAudit(OrderAudit.PsCancelOrderFinished);
        // }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new Exception($"It is not possible to change the order status from {OrderStatus.Message} to {orderStatusToChange.Message}.");
        }
    }
}
