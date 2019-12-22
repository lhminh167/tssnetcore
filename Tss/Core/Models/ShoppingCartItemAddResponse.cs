using System.Collections.Generic;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ShoppingCartItemAddResponse
    {
        public ShoppingCart Item { get; set; }           
        public string Token { get; set; }
    }
}
