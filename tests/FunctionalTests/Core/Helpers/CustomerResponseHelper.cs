using System.ComponentModel.DataAnnotations;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class CustomerResponseHelper
    {
        public static CustomerResponse MockCustomer(int customerId)
        {
            var randomText = RandomGeneratorHelper.RandomString(5);
            var randomNum = RandomGeneratorHelper.RandomNumber(10);
            var newPassword = RandomGeneratorHelper.RandomNumber(5);
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
