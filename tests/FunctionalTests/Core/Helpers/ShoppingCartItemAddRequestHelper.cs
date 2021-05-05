using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using FluentValidation;
using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Web.Fixtures;
using Tss.FunctionalTests.Web.Urls;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ShoppingCartItemAddRequestHelper
    {
        private readonly WebFixture _fixture;

        public ShoppingCartItemAddRequestHelper(
            WebFixture fixture
        )
        {
            _fixture = fixture;
        }

        public async Task<Response<ShoppingCartItemAddResponse>> Add(
            bool buyNow,
            ProductResponse product = null
        )
        {
            return await _fixture.PostAsync<ShoppingCartItemAddResponse>(
                new ShoppingCartUrl().Index,
                Mock(product ?? _fixture.TestProduct, 1, buyNow),
                HttpStatusCode.Created
            );
        }

        public ShoppingCartItemAddRequest Mock(
            ProductResponse product,
            int quantity,
            bool buyNow
        )
        {
            return new ShoppingCartItemAddRequest(
                product.ProductId,
                quantity,
                GetAttributes(product),
                buyNow
            );
        }

        private string GetAttributes(ProductResponse product)
        {
            return Encode(new List<AttributeValueResponse>{
                GetLastAttributeValue(product, "Color"),
                GetLastAttributeValue(product, "Size")
            });
        }

        private AttributeValueResponse GetLastAttributeValue(ProductResponse product, string attributeName)
        {
            return product.Attributes.Where(e => e.Name == attributeName).LastOrDefault();
        }

        private string Encode(List<AttributeValueResponse> attributeList)
        {
            Guard.Against.Null(attributeList, nameof(attributeList));

            string attributes = string.Join("/", attributeList.Select(av => av.Name));
            string attributeValues = string.Join("/", attributeList.Select(av => av.Value));
            return string.Join(":", attributes, attributeValues);
        }
    }
}
