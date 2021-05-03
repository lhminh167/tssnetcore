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
            await SetProductFilter();
            await SetTestAdminLoginToken();
            await SetTestProduct();
            await SetTestCustomer();
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
            SetAuthHeader(TestAdminLoginToken);
            var response = await PostAsync<ProductResponse>(
                new ProductUrl().Index,
                new ProductCreateRequestHelper(this).Mock(),
                HttpStatusCode.Created
            );
            var detailResponse = await GetAsync<ProductResponse>(
                new ProductUrl().Detail(response.Content.ProductId),
                HttpStatusCode.OK
            );
            TestProduct = detailResponse.Content;
        }

        private async Task SetTestCustomer()
        {
            if (TestCustomer != null) return;

            var customerUrl = new CustomerUrl();

            var response = await PostAsync<LoginResponse>(
                customerUrl.Index,
                new CustomerCreateRequestHelper().Mock(),
                HttpStatusCode.Created
            );
            TestCustomerLoginToken = response.Content.LoginToken;
            SetAuthHeader(TestCustomerLoginToken);

            var customerId = response.Content.Customer.CustomerId;
            var customerDetailUrl = customerUrl.Detail(customerId);

            await PutAsync<CustomerResponse>(
                customerDetailUrl,
                new CustomerResponseHelper().Mock(customerId),
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
            SetAuthHeader(null);
        }
    }
}
