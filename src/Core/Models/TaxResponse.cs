using System.Collections.Generic;
using System.Linq;
using Tss.Core.Entities;

namespace Tss.Core.Models
{
    public class TaxResponse
    {
        public TaxResponse()
        {
            // Orders = new HashSet<OrderResponse>();
        }

        public TaxResponse(Tax input)
        {
            if (input is null) return;

            TaxId = input.TaxId;
            TaxType = input.TaxType;
            TaxPercentage = input.TaxPercentage;
            // Orders = input.Orders.Select(e => new OrderResponse(e));
        }

        public int TaxId { get; set; }
        public string TaxType { get; set; }
        public double TaxPercentage { get; set; }

        // public virtual ICollection<OrderResponse> Orders { get; set; }
    }
}
