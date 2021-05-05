using FluentValidation;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class CustomerPasswordUpdateRequestHelper
    {
        public CustomerPasswordUpdateRequest Mock(CustomerCreateRequest input, int customerId)
        {
            var randomText = new RandomGeneratorHelper().RandomString(5);
            return new CustomerPasswordUpdateRequest
            {
                CustomerId = customerId,
                Password = input.Password,
                NewPassword = randomText,
            };
        }
    }
}
