using Tss.Core.Entities;
using System;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;

namespace Tss.Core.Services
{
    public partial class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<AttributeValue> _attributeValueRepo;
        private readonly IRepository<ShoppingCart> _shoppingCartRepo;
        private readonly IEmailHelper _emailHelper;
        private readonly IAuthHelper _authHelper;

        public CustomerService(
            IUnitOfWork unitOfWork,
            IRepository<Customer> customerRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IRepository<ShoppingCart> shoppingCartRepo,
            IEmailHelper emailHelper,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _customerRepo = customerRepo ?? throw new ArgumentNullException(nameof(customerRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new ArgumentNullException(nameof(attributeValueRepo));
            _shoppingCartRepo = shoppingCartRepo ?? throw new ArgumentNullException(nameof(shoppingCartRepo));
            _emailHelper = emailHelper ?? throw new ArgumentNullException(nameof(emailHelper));
            _authHelper = authHelper ?? throw new ArgumentNullException(nameof(authHelper));
        }

        public async Task<List<Customer>> Get()
        {
            return await _customerRepo.Get();
        }

        public async Task<Customer> Get(int customerId)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByIdSpecification(customerId, _authHelper));
            Guard.Against.NotFound(customer, "User");

            return customer;
        }

        public async Task AssociateShoppingCartToCustomer(int customerId)
        {
            string cartId = _authHelper.GetShoppingCartId();
            if (string.IsNullOrWhiteSpace(cartId)) return;

            List<ShoppingCart> items = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(new ShoppingCartGetRequest(null), cartId));
            if (items.Count == 0) return;

            items.ForEach(i => i.AssociateToCustomer(customerId));
            _shoppingCartRepo.PutRange(items);
        }

        public async Task<LoginResponse> Login(LoginRequest input)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByEmailSpecification(input.Email));
            Guard.Against.IncorrectLoginInfo(input, customer);

            await AssociateShoppingCartToCustomer(customer.CustomerId);
            await _unitOfWork.SaveAsync();

            return new LoginResponse(customer, _authHelper);
        }

        public async Task<LoginResponse> Create(CustomerCreateRequest input)
        {
            Customer duplicateCustomer = await _customerRepo.GetOne(new GetCustomerByEmailSpecification(input.Email));
            Guard.Against.EmailAddressTaken(duplicateCustomer);

            AttributeValue role = await _attributeValueRepo.GetOne(new GetAttributeValueByNameAndValueSpecification(nameof(Role), input.Role, _authHelper));
            Guard.Against.NotFound(role, nameof(Role));

            Customer customer = new Customer(input);
            _customerRepo.Post(customer);
            await _unitOfWork.SaveAsync();
            customer.AddRole(role, _authHelper);

            await AssociateShoppingCartToCustomer(customer.CustomerId);

            await _unitOfWork.SaveAsync();
            return new LoginResponse(customer, _authHelper);
        }

        public async Task<Customer> Update(CustomerUpdateRequest input)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByIdSpecification(input.CustomerId, _authHelper));
            Guard.Against.NotFound(customer, "User");

            AttributeValue role = await _attributeValueRepo.GetOne(new GetAttributeValueByNameAndValueSpecification(nameof(Role), input.Role, _authHelper));
            Guard.Against.NotFound(role, nameof(Role));

            if (input.Email != customer.Email)
            {
                Customer duplicateCustomer = await _customerRepo.GetOne(new GetCustomerByEmailSpecification(input.Email));
                Guard.Against.EmailAddressTaken(duplicateCustomer);
            }

            customer.UpdateInfo(input);
            customer.UpdateRole(role, _authHelper);
            _customerRepo.Put(customer);
            await _unitOfWork.SaveAsync();
            return customer;
        }

        public async Task<Customer> Delete(int customerId)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByIdSpecification(customerId, _authHelper));
            Guard.Against.NotFound(customer, "User");


            _customerRepo.Delete(customer);
            await _unitOfWork.SaveAsync();
            return customer;
        }
    }
}