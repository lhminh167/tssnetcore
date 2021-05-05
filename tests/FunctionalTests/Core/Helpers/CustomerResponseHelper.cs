using System.ComponentModel.DataAnnotations;
using Tss.FunctionalTests.Core.Entities;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class CustomerResponseHelper
    {
        public CustomerResponse Mock(int customerId)
        {
            var randomText = new RandomGeneratorHelper().RandomString(5);
            var randomNum = new RandomGeneratorHelper().RandomNumber(10);
            var newPassword = new RandomGeneratorHelper().RandomNumber(5);
            var res = new CustomerResponse
            {
                // ...defaultCustomer,
                CustomerId = customerId,
                Name = randomText,
                Email = randomText + "@mail.com",
                MobPhone = randomNum,
                DayPhone = randomNum,
                EvePhone = randomNum,
                PostalCode = randomText,
                City = randomText,
                Region = randomText,
                Role = Role.Customer.Message,
                Address1 = randomText,
                Address2 = randomText,
                Country = randomText,
                ShippingRegionId = 2,
                // Password = randomText,
                // NewPassword,
                // ConfirmNewPassword = newPassword
            };

            return res;

        }

    }
}
