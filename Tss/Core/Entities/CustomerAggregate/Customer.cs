using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
using Tss.Core.Models;
using Tss.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Tss.Core.Entities
{
    public class Customer : IAggregateRoot
    {
        public Customer(CustomerCreateRequest input)
        {
            Guard.Against.Null(input, nameof(CustomerCreateRequest));
            Guard.Against.NullOrWhiteSpace(input.Name, nameof(input.Name));
            Guard.Against.NullOrWhiteSpace(input.Email, nameof(input.Email));
            Guard.Against.NullOrWhiteSpace(input.Password, nameof(input.Password));

            CustomerAttribute = new List<CustomerAttribute>();
            Orders = new List<Orders>();
            Review = new List<Review>();
            SetPassword(input.Password);
            Name = input.Name;
            Email = input.Email;
        }

        public Customer()
        {
            CustomerAttribute = new HashSet<CustomerAttribute>();
            Orders = new HashSet<Orders>();
            Review = new HashSet<Review>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string CreditCard { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int ShippingRegionId { get; set; }
        public string DayPhone { get; set; }
        public string EvePhone { get; set; }
        public string MobPhone { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime ResetPasswordExpiryDate { get; set; }

        public virtual ShippingRegion ShippingRegion { get; set; }
        public virtual ICollection<CustomerAttribute> CustomerAttribute { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Review> Review { get; set; }

        public async Task ForgotPassword(IEmailHelper emailHelper)
        {
            Guard.Against.Null(emailHelper, nameof(emailHelper));

            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] byteArray = new byte[20];
            provider.GetBytes(byteArray);

            ResetPasswordToken = BitConverter.ToString(byteArray, 0);
            ResetPasswordExpiryDate = DateTime.Now.AddDays(1); //expires in one day

            await emailHelper.SendResetPasswordEmail(this);
        }

        public void ValidateResetPasswordToken(CustomerPasswordResetTokenValidationRequest input)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.InvalidResetPasswordToken(
                ResetPasswordToken != input.ResetPasswordToken
                || DateTime.Now > ResetPasswordExpiryDate
            );
        }

        public async Task ResetForgottenPassword(CustomerPasswordResetRequest input, IEmailHelper emailHelper)
        {
            Guard.Against.Null(emailHelper, nameof(emailHelper));
            Guard.Against.Null(input, nameof(input));
            ValidateResetPasswordToken(input);

            SetPassword(input.NewPassword);
            await emailHelper.SendPasswordChangeConfirmationEmail(this);
        }

        public void UpdatePassword(CustomerPasswordUpdateRequest input, IAuthHelper authHelper)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.Unauthorized(CustomerId != authHelper.GetCustomerId());
            Guard.Against.IncorrectCurrentPassword(input.CurrentPassword, this);
            Guard.Against.BadRequest(input.CurrentPassword == input.NewPassword, "New password should be differrent from current password.");

            SetPassword(input.NewPassword);
        }

        private void SetPassword(string password)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool IsCorrectPassword(string password)
        {
            Guard.Against.NullOrWhiteSpace(password, nameof(password));


            // byte[] saltBytes = Encoding.UTF8.GetBytes(PasswordSalt);
            // byte[] hashBytes = Encoding.UTF8.GetBytes(PasswordHash);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // if (hashBytes.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            // if (saltBytes.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (HMACSHA512 hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(passwordBytes);
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != PasswordHash[i]) return false;
                }
            }

            return true;
        }

        public void UpdateInfo(CustomerUpdateRequest input)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.NullOrWhiteSpace(input.Name, nameof(input.Name));
            Guard.Against.NullOrWhiteSpace(input.Email, nameof(input.Email));
            Guard.Against.NullOrWhiteSpace(input.Address1, nameof(input.Address1));
            Guard.Against.NullOrWhiteSpace(input.Address2, nameof(input.Address2));
            Guard.Against.NullOrWhiteSpace(input.City, nameof(input.City));
            Guard.Against.NullOrWhiteSpace(input.Region, nameof(input.Region));
            Guard.Against.NullOrWhiteSpace(input.PostalCode, nameof(input.PostalCode));
            Guard.Against.NullOrWhiteSpace(input.Country, nameof(input.Country));
            Guard.Against.OutOfRange(input.ShippingRegionId, nameof(input.ShippingRegionId), 1, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(input.DayPhone, nameof(input.DayPhone));
            Guard.Against.NullOrWhiteSpace(input.EvePhone, nameof(input.EvePhone));
            Guard.Against.NullOrWhiteSpace(input.MobPhone, nameof(input.MobPhone));

            Name = input.Name;
            Email = input.Email;
            Address1 = input.Address1;
            Address2 = input.Address2;
            City = input.City;
            Region = input.Region;
            PostalCode = input.PostalCode;
            Country = input.Country;
            ShippingRegionId = input.ShippingRegionId;
            DayPhone = input.DayPhone;
            EvePhone = input.EvePhone;
            MobPhone = input.MobPhone;
        }

        public void UpdateInfoOnCheckout(OrderCreateRequest input)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.NullOrWhiteSpace(input.Name, nameof(input.Name));
            Guard.Against.NullOrWhiteSpace(input.Address1, nameof(input.Address1));
            Guard.Against.NullOrWhiteSpace(input.Address2, nameof(input.Address2));
            Guard.Against.NullOrWhiteSpace(input.City, nameof(input.City));
            Guard.Against.NullOrWhiteSpace(input.Region, nameof(input.Region));
            Guard.Against.NullOrWhiteSpace(input.PostalCode, nameof(input.PostalCode));
            Guard.Against.NullOrWhiteSpace(input.Country, nameof(input.Country));
            Guard.Against.OutOfRange(input.ShippingRegionId, nameof(input.ShippingRegionId), 1, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(input.DayPhone, nameof(input.DayPhone));
            Guard.Against.NullOrWhiteSpace(input.EvePhone, nameof(input.EvePhone));
            Guard.Against.NullOrWhiteSpace(input.MobPhone, nameof(input.MobPhone));

            Name = input.Name;
            Address1 = input.Address1;
            Address2 = input.Address2;
            City = input.City;
            Region = input.Region;
            PostalCode = input.PostalCode;
            Country = input.Country;
            ShippingRegionId = input.ShippingRegionId;
            DayPhone = input.DayPhone;
            EvePhone = input.EvePhone;
            MobPhone = input.MobPhone;
        }


        private void AddCustomerAttribute(AttributeValue attributeValue)
        {
            CustomerAttribute?.Add(new CustomerAttribute(this, attributeValue));
        }

        private void UpdateCustomerAttribute(CustomerAttribute customerAttribute, AttributeValue attributeValue)
        {
            CustomerAttribute = new List<CustomerAttribute>();
            AddCustomerAttribute(attributeValue);
        }

        public string GetRole()
        {
            return CustomerAttribute?
                .FirstOrDefault(ca => ca.AttributeValue.Attribute.Name == "Role")
                .AttributeValue.Value;
        }

        public void UpdateRole(AttributeValue role, IAuthHelper authHelper)
        {
            AllowToAddRole(role, authHelper);

            CustomerAttribute customerAttribute = CustomerAttribute?.FirstOrDefault(ca => ca.AttributeValue.Attribute.Name == "Role");
            UpdateCustomerAttribute(customerAttribute, role);
        }

        public void AddRole(AttributeValue role, IAuthHelper authHelper)
        {
            AllowToAddRole(role, authHelper);

            AddCustomerAttribute(role);
        }

        public void AllowToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            AllowAdminToAddRole(role, authHelper);
            AllowCustomerToAddRole(role, authHelper);
            AllowAnonymousUserToAddRole(role, authHelper);
        }

        private void AllowAdminToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            bool isAdmin =
                authHelper.GetRole() == "Admin" &&
                authHelper.GetCustomerId() > 0;

            if (!isAdmin) return;
        }

        private void AllowCustomerToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            int customerId = authHelper.GetCustomerId();

            bool IsCustomer =
                authHelper.GetRole() == "Customer" &&
                customerId > 0;

            if (!IsCustomer) return;

            bool isAuthorized =
                CustomerId == customerId
                && role.Value == "Customer";

            Guard.Against.Unauthorized(!isAuthorized);
        }

        private void AllowAnonymousUserToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            int customerId = authHelper.GetCustomerId();

            bool isAnonymousUser =
                authHelper.GetRole() == "Customer" &&
                customerId == 0;

            if (!isAnonymousUser) return;

            bool isAuthorized =
                role.Value == "Customer";

            Guard.Against.Unauthorized(!isAuthorized);
        }
    }
}
