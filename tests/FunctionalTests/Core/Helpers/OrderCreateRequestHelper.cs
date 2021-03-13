using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class OrderCreateRequestHelper
    {
        public OrderCreateRequest Mock(
            int customerId,
            string PaymentMethodId
        )
        {
            var randomText = RandomGeneratorHelper.RandomString(5);
            return new OrderCreateRequest
            {
                Comments = randomText,
                ShippingId = 2,
                CustomerId = customerId,
                Name = randomText,
                // Email = randomText + "@mail.com",
                Address1 = randomText,
                Address2 = randomText,
                City = randomText,
                Region = randomText,
                PostalCode = randomText,
                Country = randomText,
                ShippingRegionId = 2,
                DayPhone = randomText,
                EvePhone = randomText,
                MobPhone = randomText,
                PaymentMethodId = PaymentMethodId,
                PaymentId = randomText,
                PaymentSecret = randomText,
            };
        }

        public string SuccessfulPaymentMethod = "pm_card_us";
    }
}
