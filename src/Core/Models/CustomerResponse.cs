using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class CustomerResponse
    {
        public CustomerResponse(
            Customer input,
            bool withoutAttributes = false
        )
        {
            Guard.Against.Null(input, nameof(input));

            CustomerId = input.CustomerId;
            Name = input.Name;
            Email = input.Email;
            CreditCard = input.CreditCard;
            Address1 = input.Address1;
            Address2 = input.Address2;
            City = input.City;
            Region = input.Region;
            PostalCode = input.PostalCode;
            Country = input.Country;
            ShippingRegion = input.ShippingRegion;
            DayPhone = input.DayPhone;
            EvePhone = input.EvePhone;
            MobPhone = input.MobPhone;
            if (withoutAttributes) return;
            Role = input.CustomerAttribute
                .FirstOrDefault(e => e.AttributeValue.Attribute.Name == nameof(Role))
                .AttributeValue.Value;
        }

        public CustomerResponse(
            Customer input,
            AttributeValue role
        )
        {
            Guard.Against.Null(input, nameof(input));

            CustomerId = input.CustomerId;
            Name = input.Name;
            Email = input.Email;
            CreditCard = input.CreditCard;
            Address1 = input.Address1;
            Address2 = input.Address2;
            City = input.City;
            Region = input.Region;
            PostalCode = input.PostalCode;
            Country = input.Country;
            ShippingRegion = input.ShippingRegion;
            DayPhone = input.DayPhone;
            EvePhone = input.EvePhone;
            MobPhone = input.MobPhone;
            Role = role.Value;
        }

        public int CustomerId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CreditCard { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public ShippingRegion ShippingRegion { get; private set; }
        public string DayPhone { get; private set; }
        public string EvePhone { get; private set; }
        public string MobPhone { get; private set; }
        public string Role { get; private set; }
    }
}
