using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
using Tss.Core.Models;
using Tss.Core.Interfaces;

namespace Tss.Core.Entities
{
    public class Customer : IAggregateRoot
    {
        public Customer(RegistrationForm input, AttributeValue role, IAuthHelper authHelper)
        {
            CustomerAttribute = new List<CustomerAttribute>();
            Orders = new List<Orders>();
            Review = new List<Review>();
            
            SetPassword(input.Password);
            AddInfo(input);
            AddRole(role, authHelper);

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

        public virtual ShippingRegion ShippingRegion { get; set; }
        public virtual ICollection<CustomerAttribute> CustomerAttribute { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Review> Review { get; set; }

        public void SetPassword(string password)
        {
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

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

        public void UpdateInfo(RegistrationForm input)
        {
            Guard.Against.Null(input, nameof(input));

            Name = ValidateAndUpdateString(Name, input.Name);
            Email = ValidateAndUpdateString(Email, input.Email);
            Address1 = ValidateAndUpdateString(Address1, input.Address1);
            Address2 = ValidateAndUpdateString(Address2, input.Address2);
            City = ValidateAndUpdateString(City, input.City);
            Region = ValidateAndUpdateString(Region, input.Region);
            PostalCode = ValidateAndUpdateString(PostalCode, input.PostalCode);
            Country = ValidateAndUpdateString(Country, input.Country);
            ShippingRegionId = input.ShippingRegionId > 0 ? input.ShippingRegionId : ShippingRegionId;
            DayPhone = ValidateAndUpdateString(DayPhone, input.DayPhone);
            EvePhone = ValidateAndUpdateString(EvePhone, input.EvePhone);
            MobPhone = ValidateAndUpdateString(MobPhone, input.MobPhone);
            if (!IsCorrectPassword(input.Password)) SetPassword(input.Password);
        }

        private string ValidateAndUpdateString(string currentValue, string newValue)
        {
            Guard.Against.NullOrWhiteSpace(currentValue, nameof(currentValue));
            Guard.Against.NullOrWhiteSpace(newValue, nameof(newValue));

            return newValue == currentValue ? currentValue : newValue;
        }

        public void AddInfo(RegistrationForm input)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.OutOfRange(input.ShippingRegionId, nameof(input.ShippingRegionId), 1, int.MaxValue);

            Name = ValidateAndAddString(input.Name);
            Email = ValidateAndAddString(input.Email);
            Address1 = ValidateAndAddString(input.Address1);
            Address2 = ValidateAndAddString(input.Address2);
            City = ValidateAndAddString(input.City);
            Region = ValidateAndAddString(input.Region);
            PostalCode = ValidateAndAddString(input.PostalCode);
            Country = ValidateAndAddString(input.Country);
            ShippingRegionId = input.ShippingRegionId > 0 ? input.ShippingRegionId : ShippingRegionId;
            DayPhone = ValidateAndAddString(input.DayPhone);
            EvePhone = ValidateAndAddString(input.EvePhone);
            MobPhone = ValidateAndAddString(input.MobPhone);
            SetPassword(input.Password);
        }


        private string ValidateAndAddString(string value)
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(value));

            return value;
        }

        private void AddCustomerAttribute(AttributeValue attributeValue)
        {
            Guard.Against.Null(attributeValue, nameof(attributeValue));

            CustomerAttribute?.Add(new CustomerAttribute(this, attributeValue));
        }

        public void UpdateCustomerAttribute(CustomerAttribute customerAttribute, AttributeValue attributeValue)
        {
            Guard.Against.Null(customerAttribute, nameof(customerAttribute));
            Guard.Against.Null(attributeValue, nameof(attributeValue));

            CustomerAttribute?.Remove(customerAttribute);
            AddCustomerAttribute(attributeValue);
        }

        private List<AttributeValueResponse> GetAttributes()
        {
            return CustomerAttribute?
                .Select(ca => new AttributeValueResponse(ca.AttributeValue))
                .ToList();
        }

        public string GetRole()
        {
            return CustomerAttribute?
                .FirstOrDefault(ca => ca.AttributeValue.Attribute.Name == "Role")
                .AttributeValue.Value;
        }

        public void UpdateRole(AttributeValue role, IAuthHelper authHelper)
        {
            Guard.Against.Null(role, nameof(role));
            AllowToAddRole(role, authHelper);

            CustomerAttribute customerAttribute = CustomerAttribute?.FirstOrDefault(ca => ca.AttributeValue.Attribute.Name == "Role");
            UpdateCustomerAttribute(customerAttribute, role);
        }

        private void AddRole(AttributeValue role, IAuthHelper authHelper)
        {
            Guard.Against.Null(role, nameof(role));
            Guard.Against.Null(authHelper, nameof(authHelper));
            AllowToAddRole(role, authHelper);

            AddCustomerAttribute(role);
        }

        private void AllowToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            Guard.Against.Null(role, nameof(role));
            Guard.Against.Null(authHelper, nameof(authHelper));

            AllowAdminToAddRole(role, authHelper);
            AllowSupplierToAddRole(role, authHelper);
            AllowCustomerToAddRole(role, authHelper);
            AllowAnonymousUserToAddRole(role, authHelper);
        }

        private void AllowAdminToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            Guard.Against.Null(role, nameof(role));
            Guard.Against.Null(authHelper, nameof(authHelper));
            if (authHelper.GetRole() != "Admin") return;

            bool isAuthorized = role.Value == "Admin";

            Guard.Against.Unauthorized(!isAuthorized);
        }

        private void AllowSupplierToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            Guard.Against.Null(role, nameof(role));
            Guard.Against.Null(authHelper, nameof(authHelper));
            if (authHelper.GetRole() != "Supplier") return;

            bool isAuthorized =
                CustomerId == authHelper.GetCustomerId()
                && role.Value == "Customer";

            Guard.Against.Unauthorized(!isAuthorized);
        }

        private void AllowCustomerToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            Guard.Against.Null(role, nameof(role));
            Guard.Against.Null(authHelper, nameof(authHelper));
            if (authHelper.GetRole() != "Customer") return;

            bool isAuthorized =
                CustomerId == authHelper.GetCustomerId()
                && role.Value == "Supplier";

            Guard.Against.Unauthorized(!isAuthorized);
        }

        private void AllowAnonymousUserToAddRole(AttributeValue role, IAuthHelper authHelper)
        {
            Guard.Against.Null(role, nameof(role));
            Guard.Against.Null(authHelper, nameof(authHelper));
            if (authHelper.GetCustomerId() > 0) return;

            bool isAuthorized = 
                role.Value == "Customer"
                || role.Value == "Supplier";

            Guard.Against.Unauthorized(isAuthorized);
        }
    }
}
