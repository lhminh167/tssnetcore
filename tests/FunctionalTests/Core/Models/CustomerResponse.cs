using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;
using Tss.Core.Interfaces;
using System;
using System.Threading.Tasks;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class CustomerResponse
    {
        public CustomerResponse()
        {
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
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
        public string Role { get; set; }

        public virtual ShippingRegionResponse ShippingRegion { get; set; }
    }
}
