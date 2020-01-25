using System.Collections.Generic;
using System.Linq;

namespace Tss.Core.Models
{
    public class ShoppingCartItemAddRequest
    {        
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Attributes { get; set; }
    }
}
