using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using System.Collections.Generic;

namespace Tss.FunctionalTests.Web.ControllerTests.Product.GetBy
{
    [Collection("Startup collection")]
    public class Category
    {
        private readonly WebFixture _fixture;

        public Category(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "4")]
        public async Task ReturnsOkResult()
        {
            var categories = new List<string> { "Animal", "Love" };
            var request = new ProductGetRequest(
                0, 8, true, null, true, null, null, null,
                categories,
                0, 0, 0
            );

            var response = await _fixture.GetAsync<PaginatedList<Core.Entities.Product>>(
                new ProductUrl().List(request),
                HttpStatusCode.OK
            );

            var product = response.Content.Items[0];
            var detailReponse = await _fixture.GetAsync<ProductResponse>(
                new ProductUrl().Detail(product.ProductId),
                HttpStatusCode.OK
            );
            Assert.True(categories.Contains(detailReponse.Content.Category.Name));
        }
    }
}
