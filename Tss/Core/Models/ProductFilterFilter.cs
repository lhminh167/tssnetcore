using System.Collections.Generic;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class ProductFilterFilter
    {
        public List<ProductFilterAttribute> Attributes { get; set; }
        public List<ProductFilterCategory> Categories { get; set; }
    }
}
