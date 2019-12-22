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
        private readonly IEmailHelper _emailHelper;
        private readonly IAuthHelper _authHelper;

        public CustomerService(
            IUnitOfWork unitOfWork,
            IRepository<Customer> customerRepo,
            IRepository<AttributeValue> attributeValueRepo,
            IEmailHelper emailHelper,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _customerRepo = customerRepo ?? throw new ArgumentNullException(nameof(customerRepo));
            _attributeValueRepo = attributeValueRepo ?? throw new ArgumentNullException(nameof(attributeValueRepo));
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

        public async Task<LoginResponse> Login(LoginForm input)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByEmailSpecification(input.Email));
            Guard.Against.IncorrectLoginInfo(input, customer);

            string token = _authHelper.GetLoginToken(customer);
            return new LoginResponse(token);
        }

        public async Task<Customer> Create(RegistrationForm input)
        {
            Customer duplicateCustomer = await _customerRepo.GetOne(new GetCustomerByEmailSpecification(input.Email));
            Guard.Against.EmailAddressTaken(duplicateCustomer);
            
            AttributeValue role = await _attributeValueRepo.GetOne(new GetAttributeValueByNameAndValueSpecification(nameof(Role), null, _authHelper));
            Guard.Against.NotFound(role, nameof(Role));

            Customer customer = new Customer(input, role, _authHelper);
            _customerRepo.Post(customer);
            await _unitOfWork.SaveAsync();
            return customer;
        }

        public async Task<Customer> Update(RegistrationForm input)
        {
            Customer customer = await _customerRepo.GetOne(new GetCustomerByIdSpecification(input.CustomerId, _authHelper));
            Guard.Against.NotFound(customer, "User");

            AttributeValue role = await _attributeValueRepo.GetOne(new GetAttributeValueByNameAndValueSpecification(nameof(Role), input.Role, _authHelper));
            Guard.Against.NotFound(role, nameof(Role));

            if (input.Email != customer.Email) {
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