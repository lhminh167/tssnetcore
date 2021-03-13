using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using FluentValidation;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class ShoppingCartItemAddRequestHelper
    {
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
