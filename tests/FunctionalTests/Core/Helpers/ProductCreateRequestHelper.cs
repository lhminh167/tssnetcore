using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Tss.Core.Extensions;
using Tss.FunctionalTests.Core.Entities;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ProductCreateRequestHelper
    {

        private readonly WebFixture _fixture;

        public ProductCreateRequestHelper(
            WebFixture fixture
        )
        {
            _fixture = fixture;
        }


        public ProductCreateRequest Mock()
        {
            var randomText = new RandomGeneratorHelper().RandomString(5);
            var randomNumber = new RandomGeneratorHelper().RandomNumber(2).ToInt();
            var color = _fixture.TestProductFilter.Attributes.LastOrDefault(e => e.Name == "Color").AttributeValueId;
            var size = _fixture.TestProductFilter.Attributes.LastOrDefault(e => e.Name == "Size").AttributeValueId;

            return new ProductCreateRequest
            {
                Name = randomText,
                Description = randomText,
                Price = randomNumber + 34.5,
                DiscountedPrice = randomNumber,
                Image = randomText,
                Image2 = randomText,
                Thumbnail = randomText,
                Display = (short)ProductDisplay.Other.Id,
                CategoryId = _fixture.TestProductFilter.Categories.LastOrDefault().CategoryId,
                Attributes = new List<int>() { color, size }
            };
        }
    }
}
