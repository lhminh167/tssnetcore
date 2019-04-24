using System;
using System.Collections.Generic;

namespace Tss.Core
{
    public partial class Tax
    {
        public int TaxId { get; set; }
        public string TaxType { get; set; }
        public decimal TaxPercentage { get; set; }
    }
}
