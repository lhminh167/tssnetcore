using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Urls;
using System.Net;
using System;

namespace Tss.FunctionalTests.Web.Controllers
{
    public class CreatePayment : IClassFixture<StartupFixture>
    {
        private readonly WebFixture _fixture;
        private readonly ITestOutputHelper _output;

        public CreatePayment(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
            _output = output;
        }

        [Fact]
        public async Task ReturnsOkResult()
        {
            _fixture.SetAuthHeader(_fixture.TestCustomerLoginToken);
            var cartResponse = await _fixture.PostAsync<ShoppingCartItemAddResponse>(
                new ShoppingCartUrl().Index,
                new ShoppingCartItemAddRequestHelper().Mock(_fixture.TestProduct, 1, true),
                HttpStatusCode.Created
            );
            var formResponse = await _fixture.GetAsync<OrderCreateFormResponse>(
                new OrderUrl().New,
                HttpStatusCode.OK
            );

            var helper = new OrderCreateRequestHelper();
            var resquest = helper.Mock(_fixture.TestCustomer.CustomerId, helper.SuccessfulPaymentMethod);
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
            _fixture.SetAuthHeader(_fixture.TestAdminLoginToken);
            var stockOkResponse = await _fixture.PutAsync<OrderCreateResponse>(
                new OrderUrl().StockOk(orderResponse.Content.Order.OrderId),
                null,
                HttpStatusCode.OK
            );
            var shipOkResponse = await _fixture.PutAsync<OrderCreateResponse>(
                new OrderUrl().ShipOk(orderResponse.Content.Order.OrderId),
                null,
                HttpStatusCode.OK
            );
            var detailResponse = await _fixture.GetAsync<OrderResponse>(
                new OrderUrl().Detail(orderResponse.Content.Order.OrderId),
                HttpStatusCode.OK
            );
            Assert.Equal(1, 1);

        }
    }
}
