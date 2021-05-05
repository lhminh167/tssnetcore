using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Xunit;
using Tss.FunctionalTests.Web.Urls;
using Xunit.Abstractions;
using System.Net;
using Tss.FunctionalTests.Core.Models;
using System.Collections.Generic;
using Tss.FunctionalTests.Core.Entities;
using System.Linq;
using System;

namespace Tss.FunctionalTests.Web.ControllerTests.Product
{
    [Collection("Startup collection")]
    public class SortBy
    {
        private readonly WebFixture _fixture;

        public SortBy(
            StartupFixture factory,
            ITestOutputHelper output
        )
        {
            _fixture = factory.CreateClient(output);
        }


        [Theory, Trait("Priority", "5")]
        [InlineData("Name", true)]
        [InlineData("Name", false)]
        [InlineData("Price", true)]
        [InlineData("Price", false)]
        [InlineData("BestSelling", true)]
        [InlineData("BestSelling", false)]
        [InlineData("MostDiscounted", true)]
        [InlineData("MostDiscounted", false)]
        [InlineData("TopRated", true)]
        [InlineData("TopRated", false)]
        [InlineData("Newest", true)]
        [InlineData("Newest", false)]
        public async Task ReturnsOkResult(string sortBy, bool isDescending)//done
        {
            var request = new ProductGetRequest(
                0, 8, true,
                ProductSortBy.FromDisplayName<ProductSortBy>(sortBy).Message, isDescending,
                null, null, null,
                new List<string> { },
                0, 0, 0, true
            );

            var response = await _fixture.GetAsync<PaginatedList<Core.Entities.Product>>(
                new ProductUrl().List(request),
                HttpStatusCode.OK
            );

            var sortByFunc = GetSortByFunc(sortBy);
            var list = response.Content.Items;
            var orderedList = isDescending ?
                list.OrderByDescending(sortByFunc).ThenBy(e => e.ProductId) :
                list.OrderBy(sortByFunc).ThenBy(e => e.ProductId);
            Assert.Equal(list, orderedList.ToList());
        }

        private Func<Core.Entities.Product, object> GetSortByFunc(string sortBy) =>
            sortBy == ProductSortBy.Price.Message ? e =>
                e.DiscountedPrice > 0 ?
                e.DiscountedPrice :
                e.Price :
            sortBy == ProductSortBy.Name.Message ? e => e.Name :
            sortBy == ProductSortBy.TopRated.Message ? e =>
                e.Review.Count > 0 ?
                (decimal)e.Review.Average(r => (decimal)r.Rating) :
                0 :
            sortBy == ProductSortBy.MostDiscounted.Message ? e =>
                e.DiscountedPrice > 0 ?
                (e.Price - e.DiscountedPrice) / e.Price * 100.0 :
                0 :
            sortBy == ProductSortBy.Newest.Message ? e => e.ProductId :
            sortBy == ProductSortBy.BestSelling.Message ? e => e.OrderDetail.Count :
            (Func<Core.Entities.Product, object>)(e => e.ProductId);
    }
}
