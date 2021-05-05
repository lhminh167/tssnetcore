using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
using Tss.Core.Models;
using Tss.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Tss.FunctionalTests.Core.Entities
{
    public class Customer : IAggregateRoot
    {
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
    }
}
