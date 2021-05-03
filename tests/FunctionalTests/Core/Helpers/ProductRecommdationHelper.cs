using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Tss.Core.Extensions;
using Tss.FunctionalTests.Core.Entities;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;
using Tss.FunctionalTests.Web.Urls;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ProductRecommdationHelper
    {

        private readonly WebFixture _fixture;

        public ProductRecommdationHelper(
            WebFixture fixture
        )
        {
            _fixture = fixture;
        }


        public async Task<Response<ProductResponse>> Mock()
        {
            _fixture.SetAuthHeader(_fixture.TestAdminLoginToken);
            var productResquest1 = new ProductCreateRequestHelper(_fixture).Mock();
            var productResponse = await _fixture.PostAsync<ProductResponse>(
                new ProductUrl().Index,
                productResquest1,
                HttpStatusCode.Created
            );
            var detailResponse = await _fixture.GetAsync<ProductResponse>(
                new ProductUrl().Detail(productResponse.Content.ProductId),
                HttpStatusCode.OK
            );

            var customerResquest = new CustomerCreateRequestHelper().Mock();
            var customerResponse = await _fixture.PostAsync<LoginResponse>(
                new CustomerUrl().Index,
                customerResquest,
                HttpStatusCode.Created
            );
            _fixture.SetAuthHeader(customerResponse.Content.LoginToken);

            var helper = new ShoppingCartItemAddRequestHelper(_fixture);
            var itemResponse1 = await helper.Add(true);
            var itemResponse2 = await helper.Add(true, detailResponse.Content);

            var orderHelper = new OrderCreateRequestHelper();
            var resquest = orderHelper.Mock(customerResponse.Content.Customer.CustomerId, orderHelper.SuccessfulPaymentMethod);
            var paymentResponse = await _fixture.PostAsync<OrderCreateResponse>(
                new OrderUrl().Payment,
                resquest,
                HttpStatusCode.Created
            );
            resquest.PaymentId = paymentResponse.Content.PaymentId;
            resquest.PaymentSecret = paymentResponse.Content.PaymentSecret;
            var orderResponse = await _fixture.PostAsync<OrderCreateResponse>(
                new OrderUrl().Index,
                resquest,
                HttpStatusCode.Created
            );
            _fixture.SetAuthHeader(null);
            return detailResponse;
        }
    }
}
