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
    public class Keyword
    {
        private readonly WebFixture _fixture;

        public Keyword(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }

        [Theory, Trait("Priority", "4")]
        [InlineData("Name")]
        [InlineData("Description")]
        public async Task ReturnsOkResult(string searchBy)//done
        {
            var request = new ProductGetRequest(
                0, 8, true, null, true,
                searchBy == "Name" ?
                    _fixture.TestProduct.Name :
                    _fixture.TestProduct.Description,
                null, null, new List<string> { },
                0, 0, 0
            );

            var response = await _fixture.GetAsync<PaginatedList<Core.Entities.Product>>(
                new ProductUrl().List(request),
                HttpStatusCode.OK
            );

            var product = response.Content.Items[0];
            bool condition = searchBy == "Name" ?
                   product.Name == _fixture.TestProduct.Name :
                   product.Description == _fixture.TestProduct.Description;
            Assert.True(condition);
        }
    }
}
