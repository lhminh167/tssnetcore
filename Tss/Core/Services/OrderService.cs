using Tss.Core.Entities;
using System;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Ardalis.GuardClauses;

namespace Tss.Core.Services
{
    public partial class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Orders> _ordersRepo;
        private readonly IRepository<ShoppingCart> _shoppingCartRepo;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<Shipping> _shippingRepo;
        private readonly IRepository<ShippingRegion> _shippingRegionRepo;
        private readonly IRepository<Tax> _taxRepo;
        private readonly IEmailHelper _emailHelper;
        private readonly IPaymentHelper _paymentHelper;
        private readonly IAuthHelper _authHelper;

        public OrderService(
            IUnitOfWork unitOfWork,
            IRepository<Orders> ordersRepo,
            IRepository<ShoppingCart> shoppingCartRepo,
            IRepository<Customer> customerRepo,
            IRepository<Shipping> shippingRepo,
            IRepository<ShippingRegion> shippingRegionRepo,
            IRepository<Tax> taxRepo,
            IEmailHelper emailHelper,
            IPaymentHelper paymentHelper,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _ordersRepo = ordersRepo ?? throw new ArgumentNullException(nameof(ordersRepo));
            _shoppingCartRepo = shoppingCartRepo ?? throw new ArgumentNullException(nameof(shoppingCartRepo));
            _customerRepo = customerRepo ?? throw new ArgumentNullException(nameof(customerRepo));
            _shippingRepo = shippingRepo ?? throw new ArgumentNullException(nameof(shippingRepo));
            _shippingRegionRepo = shippingRegionRepo ?? throw new ArgumentNullException(nameof(shippingRegionRepo));
            _taxRepo = taxRepo ?? throw new ArgumentNullException(nameof(taxRepo));
            _emailHelper = emailHelper ?? throw new ArgumentNullException(nameof(emailHelper));
            _paymentHelper = paymentHelper ?? throw new ArgumentNullException(nameof(paymentHelper));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        public async Task<PaginatedList<Orders>> Get(PaginationRequest input)
        {
            return await _ordersRepo.GetPaginated(new GetOrderSpecification(_authHelper, input));
        }

        public async Task<Orders> Get(int orderId)
        {
            Orders order = await _ordersRepo.GetOne(new GetOrderByIdSpecification(orderId, _authHelper));
            Guard.Against.NotFound(order, nameof(order));

            return order;
        }

        public async Task<OrderCreateFormResponse> GetCreateOrderForm()
        {
            return new OrderCreateFormResponse(
                await _shippingRepo.Get(),
                await _shippingRegionRepo.Get(),
                await _taxRepo.Get()
            );
        }

        public async Task<OrderCreateResponse> Create(OrderCreateRequest input)
        {
            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(new ShoppingCartGetRequest(true), _authHelper.GetCustomerId().ToString()));
            Guard.Against.NotFound(items, nameof(ShoppingCart));


            ShippingRegion shippingRegion = await _shippingRegionRepo.Get(input.ShippingRegionId);
            Guard.Against.NotFound(shippingRegion, nameof(ShippingRegion));


            Shipping shipping = await _shippingRepo.Get(input.ShippingId);
            Guard.Against.NotFound(shipping, nameof(Shipping));


            Customer customer = await _customerRepo.Get(_authHelper.GetCustomerId());
            Guard.Against.NotFound(customer, nameof(Customer));
            customer.UpdateInfoOnCheckout(input);


            int taxId = Tax.GetTaxId(shipping);
            Tax tax = await _taxRepo.Get(taxId);
            Guard.Against.NotFound(tax, nameof(Tax));


            Orders order = new Orders(items, customer, shipping, tax, shippingRegion, _paymentHelper, input.PaymentMethodId);
            _ordersRepo.Post(order);
            await _unitOfWork.SaveAsync();


            OrderCreateResponse response = await order.PsInitialNotification(_emailHelper, _paymentHelper);
            _shoppingCartRepo.DeleteRange(items);
            await _unitOfWork.SaveAsync();

            return response;
        }

        public async Task<Orders> StockOk(int orderId)
        {
            Orders order = await _ordersRepo.GetOne(new GetOrderByIdSpecification(orderId, _authHelper));
            Guard.Against.NotFound(order, nameof(order));

            await order.PsStockOk(_emailHelper, _paymentHelper);
            _ordersRepo.Put(order);
            await _unitOfWork.SaveAsync();
            return order;
        }

        public async Task<Orders> StockNotOk(int orderId, string comments)
        {
            Orders order = await _ordersRepo.GetOne(new GetOrderByIdSpecification(orderId, _authHelper));
            Guard.Against.NotFound(order, nameof(order));

            List<ShoppingCart> cart = await order.PsStockNotOk(_emailHelper, _paymentHelper, comments);
            _shoppingCartRepo.PostRange(cart);
            _ordersRepo.Put(order);
            await _unitOfWork.SaveAsync();
            return order;
        }

        public async Task<Orders> ShipOk(int orderId)
        {
            Orders order = await _ordersRepo.GetOne(new GetOrderByIdSpecification(orderId, _authHelper));
            Guard.Against.NotFound(order, nameof(order));

            await order.PsShipOk(_emailHelper, _paymentHelper);
            _ordersRepo.Put(order);
            await _unitOfWork.SaveAsync();
            return order;
        }
    }
}