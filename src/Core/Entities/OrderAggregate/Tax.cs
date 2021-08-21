using System.Collections.Generic;

namespace Tss.Core.Entities
{
    public class Tax
    {
        private Tax()
        {
            Orders = new HashSet<Orders>();
        }

        public int TaxId { get; private set; }
        public string TaxType { get; private set; }
        public double TaxPercentage { get; private set; }

        public virtual ICollection<Orders> Orders { get; private set; }

        public static int GetTaxId(Shipping shipping)
        {
            return shipping.ShippingRegionId == 2 ? 2 : 1;
        }
    }
}
