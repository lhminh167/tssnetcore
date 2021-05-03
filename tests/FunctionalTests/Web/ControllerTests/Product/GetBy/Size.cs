using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tss.FunctionalTests.Web.ControllerTests.Product.GetBy
{
    [Collection("Startup collection")]
    public class Size
    {
        private readonly WebFixture _fixture;

        public Size(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Fact, Trait("Priority", "4")]
        public async Task ReturnsOkResult()//done
        {
            var size = _fixture.TestProductFilter.Attributes.LastOrDefault(e => e.Name == "Size");
            var request = new ProductGetRequest(
                0, 8, true, null, true, null, null, size.Value,
                new List<string> { },
                0, 0, 0
            );

            var response = await _fixture.GetAsync<PaginatedList<Core.Entities.Product>>(
                new ProductUrl().List(request),
                HttpStatusCode.OK
            );

            var product = response.Content.Items[0];
            var detailResponse = await _fixture.GetAsync<ProductResponse>(
                new ProductUrl().Detail(product.ProductId),
                HttpStatusCode.OK
            );
            Assert.True(detailResponse.Content.Attributes.Any(e => e.AttributeValueId == size.AttributeValueId));
        }
    }
}
