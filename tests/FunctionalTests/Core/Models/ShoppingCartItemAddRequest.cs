using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class ShoppingCartItemAddRequest
    {
        public ShoppingCartItemAddRequest(
            int productId,
            int quantity,
            string attributes,
            bool buyNow
        )
        {

            ProductId = productId;
            Quantity = quantity;
            Attributes = attributes;
            BuyNow = buyNow;

            new ShoppingCartItemAddRequestValidator().Validate(this);
        }

        public ShoppingCartItemAddRequest() { }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Attributes { get; set; }
        public bool BuyNow { get; set; }

        public class ShoppingCartItemAddRequestValidator : AbstractValidator<ShoppingCartItemAddRequest>
        {
            public ShoppingCartItemAddRequestValidator()
            {
                RuleFor(m => m.ProductId).GreaterThan(0);
                RuleFor(m => m.Quantity).GreaterThan(0);
                RuleFor(m => m.Attributes).NotEmpty();
                RuleFor(m => m.BuyNow).NotNull();
            }
        }
    }
}
