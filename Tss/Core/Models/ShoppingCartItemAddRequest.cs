using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace Tss.Core.Models
{
    public class ShoppingCartItemAddRequest
    {
        [Required, Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public string Attributes { get; set; }
        [Required]
        public bool? BuyNow { get; set; }

        public ShoppingCartItemAddRequest(
            int productId,
            int quantity,
            string attributes,
            bool? buyNow
        )
        {
            Guard.Against.OutOfRange(productId, nameof(productId), 1, int.MaxValue);
            Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(attributes, nameof(attributes));

            ProductId = productId;
            Quantity = quantity;
            Attributes = attributes;
            BuyNow = buyNow;
        }

    }
}
