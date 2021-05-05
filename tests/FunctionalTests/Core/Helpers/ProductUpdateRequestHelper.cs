using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Tss.Core.Extensions;
using Tss.FunctionalTests.Core.Entities;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ProductUpdateRequestHelper
    {

        private readonly WebFixture _fixture;

        public ProductUpdateRequestHelper(
            WebFixture fixture
        )
        {
            _fixture = fixture;
        }


        public ProductUpdateRequest Mock(int productId)
        {
            var randomText = new RandomGeneratorHelper().RandomString(5);
            var randomNumber = new RandomGeneratorHelper().RandomNumber(2).ToInt();
            var color = _fixture.TestProductFilter.Attributes[0].AttributeValueId;
            var size = _fixture.TestProductFilter.Attributes[0].AttributeValueId;

            return new ProductUpdateRequest
            {
                ProductId = productId,
                Name = randomText,
                Description = randomText,
                Price = randomNumber + 24.5,
                DiscountedPrice = randomNumber,
                Image = randomText,
                Image2 = randomText,
                Thumbnail = randomText,
                Display = (short)ProductDisplay.HomepageTopPanel.Id,
                CategoryId = _fixture.TestProductFilter.Categories[0].CategoryId,
                Attributes = new List<int>() { color, size }
            };
        }
    }
}
