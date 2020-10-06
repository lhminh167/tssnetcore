using Tss.Core.Entities;
using System;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using Tss.Core.Models;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Ardalis.GuardClauses;
using System.Linq;
using Tss.Infrastructure.Data;

namespace Tss.Core.Services
{
    public partial class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<AttributeValue> _attributeValueRepo;
        private readonly IRepository<ShoppingCart> _shoppingCartRepo;
        private readonly IRepository<CustomerAttribute> _customerAttributeRepo;
        private readonly IEmailHelper _emailHelper;
        private readonly IAuthHelper _authHelper;

        public CustomerService(
            IUnitOfWork unitOfWork,
            IRepository<Customer> customerRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IRepository<ShoppingCart> shoppingCartRepo,
            IRepository<CustomerAttribute> customerAttributeRepo,
            IEmailHelper emailHelper,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _customerRepo = customerRepo ?? throw new ArgumentNullException(nameof(customerRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new ArgumentNullException(nameof(attributeValueRepo));
            _shoppingCartRepo = shoppingCartRepo ?? throw new ArgumentNullException(nameof(shoppingCartRepo));
            _customerAttributeRepo = customerAttributeRepo ?? throw new ArgumentNullException(nameof(customerAttributeRepo));
            _emailHelper = emailHelper ?? throw new ArgumentNullException(nameof(emailHelper));
            _authHelper = authHelper ?? throw new ArgumentNullException(nameof(authHelper));
        }

        public async Task<List<CustomerResponse>> Get()
        {
            List<Customer> list = await _customerRepo.Get();
            return list.Select(e => new CustomerResponse(e)).ToList();
        }

        public async Task<CustomerResponse> Get(int customerId)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByIdSpecification(customerId, _authHelper));
            Guard.Against.NotFound(customer, "User");

            return new CustomerResponse(customer);
        }

        private async Task AssociateShoppingCartToCustomer(int customerId)
        {
            string cartId = _authHelper.GetShoppingCartId();
            if (string.IsNullOrWhiteSpace(cartId)) return;

            List<ShoppingCart> anonymousItems = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(new ShoppingCartGetRequest(null), cartId, false));
            if (anonymousItems.Count == 0) return;

            List<ShoppingCart> customerItems = await _shoppingCartRepo.Get(new GetShoppingCartSpecification(new ShoppingCartGetRequest(null), customerId.ToString(), false));
            customerItems.ForEach(customerItem =>
            {
                var sameItem = anonymousItems.FirstOrDefault(anonymousItem =>
                    anonymousItem.BuyNow == customerItem.BuyNow
                    && anonymousItem.Attributes == customerItem.Attributes
                    && anonymousItem.ProductId == customerItem.ProductId
                );

                if (sameItem is null) return;
                anonymousItems.Remove(sameItem);
                _shoppingCartRepo.Delete(sameItem);
                customerItem.AddUpQuantity(sameItem.Quantity);
            });
            anonymousItems.ForEach(anonymousItem => anonymousItem.AssociateToCustomer(customerId));
            _shoppingCartRepo.PutRange(anonymousItems);
            _shoppingCartRepo.PutRange(customerItems);
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

            AttributeValue role = await _attributeValueRepo.GetOne(new GetAttributeValueByNameAndValueSpecification(nameof(Role), Role.Customer.Message, _authHelper));
            Guard.Against.NotFound(role, nameof(Role));

            Customer customer = new Customer(input);
            _customerRepo.Post(customer);
            await _unitOfWork.SaveAsync();
            customer.AddRole(role, _authHelper);

            await AssociateShoppingCartToCustomer(customer.CustomerId);

            await _unitOfWork.SaveAsync();
            return new LoginResponse(customer, _authHelper);
        }

        public async Task<CustomerResponse> Update(CustomerUpdateRequest input)
        {
            Customer customer = await _customerRepo.Get(input.CustomerId);
            Guard.Against.NotFound(customer, "User");

            AttributeValue role = await _attributeValueRepo.GetOne(new GetAttributeValueByNameAndValueSpecification(nameof(Role), input.Role, _authHelper));
            Guard.Against.NotFound(role, nameof(Role));

            if (input.Email != customer.Email)
            {
                Customer duplicateCustomer = await _customerRepo.GetOne(new GetCustomerByEmailSpecification(input.Email));
                Guard.Against.EmailAddressTaken(duplicateCustomer);
            }

            customer.UpdateInfo(input);
            _customerRepo.Put(customer);
            await _unitOfWork.SaveAsync();

            await UpdateRole(customer, role);

            return new CustomerResponse(customer, role);
        }

        private async Task UpdateRole(Customer customer, AttributeValue role)
        {
            customer.AllowToAddRole(role, _authHelper);

            CustomerAttribute roleCustomerAttribute =
                await _customerAttributeRepo.GetOne(new GetCustomerAttributeSpecification(customer.CustomerId, "Role"));
            _customerAttributeRepo.Delete(roleCustomerAttribute);
            await _unitOfWork.SaveAsync();

            roleCustomerAttribute = new CustomerAttribute(customer, role, true);
            _customerAttributeRepo.Post(roleCustomerAttribute);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CustomerResponse> UpdatePassword(CustomerPasswordUpdateRequest input)
        {
            Customer customer = await _customerRepo.Get(input.CustomerId);
            Guard.Against.NotFound(customer, "User");

            customer.UpdatePassword(input, _authHelper);
            _customerRepo.Put(customer);
            await _unitOfWork.SaveAsync();
            return new CustomerResponse(customer, true);
        }

        public async Task<CustomerResponse> ForgotPassword(CustomerPasswordForgotRequest input)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByEmailSpecification(input.Email));
            Guard.Against.NotFound(customer, "User");

            await customer.ForgotPassword(_emailHelper);
            _customerRepo.Put(customer);
            await _unitOfWork.SaveAsync();
            return new CustomerResponse(customer, true);
        }

        public async Task<CustomerResponse> ValidateResetPasswordToken(CustomerPasswordResetTokenValidationRequest input)
        {
            Customer customer = await _customerRepo.Get(input.CustomerId);
            Guard.Against.NotFound(customer, "User");

            customer.ValidateResetPasswordToken(input);
            return new CustomerResponse(customer, true);
        }

        public async Task<CustomerResponse> ResetForgottenPassword(CustomerPasswordResetRequest input)
        {
            Customer customer = await _customerRepo.Get(input.CustomerId);
            Guard.Against.NotFound(customer, "User");

            await customer.ResetForgottenPassword(input, _emailHelper);
            _customerRepo.Put(customer);
            await _unitOfWork.SaveAsync();
            return new CustomerResponse(customer, true);
        }

        public async Task<CustomerResponse> Delete(int customerId)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByIdSpecification(customerId, _authHelper));
            Guard.Against.NotFound(customer, "User");

            _customerRepo.Delete(customer);
            await _unitOfWork.SaveAsync();
            return new CustomerResponse(customer);
        }
    }
}