using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public partial class Orders
    {
        public static async Task<OrderCreateResponse> CreatePayment(
            List<ShoppingCart> shoppingCart,
            Customer customer,
            Shipping shipping,
            Tax tax,
            ShippingRegion shippingRegion,
            IPaymentHelper paymentHelper,
            string paymentMethodId,
            string paymentId,
            string paymentSecret
        )
        {
            Guard.Against.Null(paymentHelper, nameof(paymentHelper));

            var order = new Orders(
                shoppingCart,
                customer,
                shipping,
                tax,
                shippingRegion
            );


            PaymentCreateResponse response = await paymentHelper.CreatePayment(
                paymentMethodId,
                "usd",
                order.TotalAmount
            );
            order.GuardAgainstUnsuccessfulPayment(OrderAudit.PsCheckFundsCreatePaymentFailed, response);


            return new OrderCreateResponse(order, response);
        }

        public static async Task<OrderCreateResponse> Create(
            List<ShoppingCart> shoppingCart,
            Customer customer,
            Shipping shipping,
            Tax tax,
            ShippingRegion shippingRegion,
            IEmailHelper emailHelper,
            IPaymentHelper paymentHelper,
            string paymentMethodId,
            string paymentId,
            string paymentSecret
        )
        {
            Guard.Against.Null(emailHelper, nameof(emailHelper));
            Guard.Against.Null(paymentHelper, nameof(paymentHelper));

            var order = new Orders(
                shoppingCart,
                customer,
                shipping,
                tax,
                shippingRegion
            );

            var response = await order.PsCheckFunds(
                emailHelper,
                paymentHelper,
                paymentMethodId,
                paymentId,
                paymentSecret
            );

            return response;
        }

        private async Task<OrderCreateResponse> PsCheckFunds(
            IEmailHelper emailHelper,
            IPaymentHelper paymentHelper,
            string paymentMethodId,
            string paymentId,
            string paymentSecret
        )
        {
            if (Status != OrderStatus.AwaitingConfirmationOfFunds.Id) StatusChangeException(OrderStatus.OrderPlacedNotifyingCustomer);
            CreateAudit(OrderAudit.OrderProcessorStarted);
            CreateAudit(OrderAudit.PsCheckFundsStarted);
            Customer.SetCreditCard(paymentMethodId);
            Reference = paymentId;
            AuthCode = paymentSecret;
            CreateAudit(OrderAudit.PsCheckFundsFundsAvailable);
            SetOrderStatus(OrderStatus.OrderPlacedNotifyingCustomer);
            CreateAudit(OrderAudit.PsCheckFundsFinished);
            return await PsInitialNotification(emailHelper, paymentHelper);
        }

        private async Task<OrderCreateResponse> PsInitialNotification(IEmailHelper emailHelper, IPaymentHelper paymentHelper)
        {
            if (Status != OrderStatus.OrderPlacedNotifyingCustomer.Id) StatusChangeException(OrderStatus.NotifyingSupplierStockCheck);
            Guard.Against.Null(emailHelper, nameof(emailHelper));
            Guard.Against.Null(paymentHelper, nameof(paymentHelper));

            CreateAudit(OrderAudit.PsInitialNotificationStarted);
            var metadata = new Dictionary<string, string>();
            metadata.Add(nameof(OrderId), OrderId.ToString());
            PaymentCreateResponse response = await paymentHelper.UpdatePayment(Reference, metadata);
            GuardAgainstUnsuccessfulPayment(OrderAudit.PsInitialNotificationUpdatePaymentFailed, response);
            await emailHelper.SendOrderInitialNotification(this);
            CreateAudit(OrderAudit.PsInitialNotificationEmailSent);
            SetOrderStatus(OrderStatus.NotifyingSupplierStockCheck);
            CreateAudit(OrderAudit.PsInitialNotificationFinished);
            await PsCheckStock(emailHelper, paymentHelper);
            return new OrderCreateResponse(this, response);
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
            Guard.Against.Null(emailHelper, nameof(emailHelper));
            Guard.Against.Null(paymentHelper, nameof(paymentHelper));

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
            var response = await paymentHelper.CompletePayment(Reference);
            GuardAgainstUnsuccessfulPayment(OrderAudit.PsTakePaymentFundsDeductFailed, response);
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
            Guard.Against.Null(emailHelper, nameof(emailHelper));
            Guard.Against.Null(paymentHelper, nameof(paymentHelper));

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
            CreateAudit(OrderAudit.OrderProcessorFinished);
        }

        public async Task<List<ShoppingCart>> PsStockNotOk(IEmailHelper emailHelper, IPaymentHelper paymentHelper, string comments)
        {
            if (Status != OrderStatus.AwaitingStockConfirmation.Id) StatusChangeException(OrderStatus.AwaitingCreditCardPayment);
            Guard.Against.Null(emailHelper, nameof(emailHelper));
            Guard.Against.Null(paymentHelper, nameof(paymentHelper));

            CreateAudit(OrderAudit.PsStockNotOkStarted);
            Comments = comments;
            await emailHelper.SendStockNotOkNotification(this);
            CreateAudit(OrderAudit.PsStockNotOkEmailSent);
            SetOrderStatus(OrderStatus.OutOfStock);
            List<ShoppingCart> cart = ConvertOrderItemsToShoppingCartItems() ?? new List<ShoppingCart>();
            CreateAudit(OrderAudit.PsOrderRevertedToShoppingCart);
            CreateAudit(OrderAudit.PsStockNotOkFinished);
            CreateAudit(OrderAudit.OrderProcessorFinished);
            return cart;
        }

        private List<ShoppingCart> ConvertOrderItemsToShoppingCartItems()
        {
            return OrderDetail.Select(item => ConvertOrderItemToShoppingCartItem(item)).ToList();
        }

        private ShoppingCart ConvertOrderItemToShoppingCartItem(OrderDetail input)
        {
            return new ShoppingCart(
                ConvertOrderItemToShoppingCartItemAddRequest(input),
                CustomerId.ToString()
            );
        }

        private ShoppingCartItemAddRequest ConvertOrderItemToShoppingCartItemAddRequest(OrderDetail input)
        {
            return new ShoppingCartItemAddRequest(
                input.ProductId,
                input.Quantity,
                input.Attributes,
                true
            );
        }

        private void GuardAgainstUnsuccessfulPayment(OrderAudit audit, PaymentCreateResponse payment)
        {
            if (payment.Status == HttpStatusCode.OK) return;
            CreateAudit(audit, payment.Title);
            Guard.Against.UnsuccessfulPayment(payment);
        }

        // private void GuardAgainstUnsuccesfulEmailSending(OrderAudit audit, PaymentCreateResponse payment)
        // {
        //     if (payment.Status == HttpStatusCode.OK) return;
        //     CreateAudit(audit, payment.Title);
        //     Guard.Against.UnsuccessfulPayment(payment);
        // }


        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new Exception($"It is not possible to change the order status from {OrderStatus.Message} to {orderStatusToChange.Message}.");
        }
    }
}
