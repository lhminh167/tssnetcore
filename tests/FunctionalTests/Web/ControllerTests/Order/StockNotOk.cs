using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Urls;
using System.Net;
using System;
using Tss.FunctionalTests.Core.Entities;

namespace Tss.FunctionalTests.Web.ControllerTests.Order
{
    [Collection("Startup collection")]
    public class StockNotOk
    {
        private readonly WebFixture _fixture;
        private readonly ITestOutputHelper _output;

        public StockNotOk(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
            _output = output;
        }

        [Fact, Trait("Priority", "1")]
        public async Task ReturnsOkResult()//done
        {
            var createResquestBody = new CustomerCreateRequestHelper().Mock();
            var createResponse = await _fixture.PostAsync<LoginResponse>(
                new CustomerUrl().Index,
                createResquestBody,
                HttpStatusCode.Created
            );
            _fixture.SetAuthHeader(createResponse.Content.LoginToken);
            var cartResponse = await new ShoppingCartItemAddRequestHelper(_fixture).Add(true);
            var helper = new OrderCreateRequestHelper();
            var resquest = helper.Mock(createResponse.Content.Customer.CustomerId, helper.SuccessfulPaymentMethod);
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
            Assert.Equal(OrderStatus.AwaitingStockConfirmation.Message, orderResponse.Content.Order.OrderStatus.Message);
            _fixture.SetAuthHeader(_fixture.TestAdminLoginToken);

            var response = await _fixture.PutAsync<OrderResponse>(
                new OrderUrl().StockNotOk(orderResponse.Content.Order.OrderId),
                new RandomGeneratorHelper().RandomString(100),
                HttpStatusCode.OK
            );

            var detailResponse = await _fixture.GetAsync<OrderResponse>(
                new OrderUrl().Detail(orderResponse.Content.Order.OrderId),
                HttpStatusCode.OK
            );
            Assert.Equal(OrderStatus.OutOfStock.Message, detailResponse.Content.OrderStatus.Message);
        }
    }
}
