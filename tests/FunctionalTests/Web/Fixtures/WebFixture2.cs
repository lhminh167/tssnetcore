using System.Threading.Tasks;
using Tss.FunctionalTests.Core.Models;
using System.Net;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Web.Urls;

namespace Tss.FunctionalTests.Web.Fixtures
{
    public partial class WebFixture
    {
        public ProductResponse TestProduct { get; private set; }
        public ProductFilterResponse TestProductFilter { get; private set; }
        public CustomerResponse TestCustomer { get; private set; }
        public string TestCustomerLoginToken { get; private set; }
        public string TestAdminLoginToken { get; private set; }

        private async Task SetTestData()
        {
            await SetTestProduct();
            await SetTestCustomer();
            await SetProductFilter();
            await SetTestAdminLoginToken();
        }

        private async Task SetProductFilter()
        {
            if (TestProductFilter != null) return;
            var response = await GetAsync<ProductFilterResponse>(
                new ProductUrl().Filter,
                HttpStatusCode.OK
            );
            TestProductFilter = response.Content;
        }

        private async Task SetTestProduct()
        {
            if (TestProduct != null) return;
            var response = await GetAsync<ProductResponse>(
                new ProductUrl().Detail(1),
                HttpStatusCode.OK
            );
            TestProduct = response.Content;
        }

        private async Task SetTestCustomer()
        {
            if (TestCustomer != null) return;

            var customerUrl = new CustomerUrl();

            var response = await PostAsync<LoginResponse>(
                customerUrl.Index,
                CustomerCreateRequestHelper.MockCustomerCreateRequest(),
                HttpStatusCode.Created
            );
            TestCustomerLoginToken = response.Content.LoginToken;
            SetAuthHeader(TestCustomerLoginToken);

            var customerId = response.Content.Customer.CustomerId;
            var customerDetailUrl = customerUrl.Detail(customerId);

            await PutAsync<CustomerResponse>(
                customerDetailUrl,
                CustomerResponseHelper.MockCustomer(customerId),
                HttpStatusCode.OK
            );

            var customerResponse = await GetAsync<CustomerResponse>(
                customerDetailUrl,
                HttpStatusCode.OK
            );
            TestCustomer = customerResponse.Content;
            SetAuthHeader(null);
        }

        private async Task SetTestAdminLoginToken()
        {
            if (TestAdminLoginToken != null) return;
            var response = await PostAsync<LoginResponse>(
                new CustomerUrl().Login,
                new LoginRequestHelper().RootAdminAccount,
                HttpStatusCode.OK
            );
            TestAdminLoginToken = response.Content.LoginToken;
            _output.WriteLine("TestAdminLoginToken " + TestAdminLoginToken);
            SetAuthHeader(null);
        }
    }
}
