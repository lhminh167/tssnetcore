using Tss.Core.Entities;
using System;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Ardalis.GuardClauses;
using AutoMapper;

namespace Tss.Core.Services
{
    public partial class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Orders> _ordersRepo;
        private readonly IRepository<ShoppingCart> _shoppingCartRepo;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<Shipping> _shippingRepo;
        private readonly IRepository<Tax> _taxRepo;
        private readonly IEmailHelper _emailHelper;
        private readonly IPaymentHelper _paymentHelper;
        private readonly IAuthHelper _authHelper;
        private readonly IMapper _mapper;

        public OrderService(
            IUnitOfWork unitOfWork,
            IRepository<Orders> ordersRepo,
            IRepository<ShoppingCart> shoppingCartRepo,
            IRepository<Customer> customerRepo,
            IRepository<Shipping> shippingRepo,
            IRepository<Tax> taxRepo,
            IEmailHelper emailHelper,
            IPaymentHelper paymentHelper,
            IAuthHelper authHelper,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _ordersRepo = ordersRepo ?? throw new ArgumentNullException(nameof(ordersRepo));
            _shoppingCartRepo = shoppingCartRepo ?? throw new ArgumentNullException(nameof(shoppingCartRepo));
            _customerRepo = customerRepo ?? throw new ArgumentNullException(nameof(customerRepo));
            _shippingRepo = shippingRepo ?? throw new ArgumentNullException(nameof(shippingRepo));
            _taxRepo = taxRepo ?? throw new ArgumentNullException(nameof(taxRepo));
            _emailHelper = emailHelper ?? throw new ArgumentNullException(nameof(emailHelper));
            _paymentHelper = paymentHelper ?? throw new ArgumentNullException(nameof(paymentHelper));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<Orders>> Get()
        {
            return await _ordersRepo.Get();
        }

        public async Task<Orders> Get(int orderId)
        {
            Orders order = await _ordersRepo.GetOne(new GetOrderSpecification(orderId, _authHelper)); 
            Guard.Against.NotFound(order, nameof(order));

            return order;
        }

        public async Task<Orders> Create(DeliveryForm input)
        {
            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(input.CartId));
            Guard.Against.NotFound(items, nameof(ShoppingCart));


            Customer customer = await _customerRepo.Get(int.Parse(input.CartId));
            Guard.Against.NotFound(customer, nameof(customer));
            RegistrationForm customerInfo = _mapper.Map<RegistrationForm>(input);
            customer.UpdateInfo(customerInfo);


            Shipping shipping = await _shippingRepo.Get(input.ShippingId);
            Guard.Against.NotFound(shipping, nameof(Shipping));


            int taxId = GetTaxId(shipping);
            Tax tax = await _taxRepo.Get(taxId);
            Guard.Against.NotFound(tax, nameof(Tax));


            Orders order = new Orders(items, customer, shipping, tax);
            await order.PsInitialNotification(_emailHelper);


            _shoppingCartRepo.DeleteRange(items);
            _ordersRepo.Post(order);


            await _unitOfWork.SaveAsync();
            return order;
        }

        private int GetTaxId(Shipping shipping)
        {
            return shipping.ShippingRegionId == 2 ? 2 : 1;
        }

        public async Task<Orders> StockOk(int orderId)
        {
            Orders order = await _ordersRepo.GetOne(new GetOrderSpecification(orderId, _authHelper));
            Guard.Against.NotFound(order, nameof(order));

            await order.PsStockOk(_emailHelper, _paymentHelper);
            _ordersRepo.Put(order);
            await _unitOfWork.SaveAsync();
            return order;
        }

        public async Task<Orders> ShipOk(int orderId)
        {
            Orders order = await _ordersRepo.GetOne(new GetOrderSpecification(orderId, _authHelper));
            Guard.Against.NotFound(order, nameof(order));

            await order.PsShipOk(_emailHelper);
            _ordersRepo.Put(order);
            await _unitOfWork.SaveAsync();
            return order;
        }
    }
}