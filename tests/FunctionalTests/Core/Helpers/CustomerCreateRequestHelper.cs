using FluentValidation;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class CustomerCreateRequestHelper
    {
        public static CustomerCreateRequest MockCustomerCreateRequest()
        {
            var randomText = RandomGeneratorHelper.RandomString(5);
            return new CustomerCreateRequest
            {
                Name = randomText,
                Email = randomText + "@mail.com",
                Password = randomText,
            };
        }



    }

}
