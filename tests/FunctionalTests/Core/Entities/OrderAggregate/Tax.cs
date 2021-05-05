using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Entities
{
    public class Tax
    {
        public Tax()
        {
            Orders = new HashSet<Orders>();
        }

        public int TaxId { get; set; }
        public string TaxType { get; set; }
        public double TaxPercentage { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

        public static int GetTaxId(Shipping shipping)
        {
            return shipping.ShippingRegionId == 2 ? 2 : 1;
        }
    }
}
