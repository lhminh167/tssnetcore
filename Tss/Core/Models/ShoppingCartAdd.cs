using System.Collections.Generic;
using System.Linq;

namespace Tss.Core.Models
{
    public class ShoppingCartAdd
    {        
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Attributes { get; set; }
        
        public List<string> DecodeAttributes() {
            return Attributes.Split(":").LastOrDefault().Split("/").ToList();
        }

    }
}
