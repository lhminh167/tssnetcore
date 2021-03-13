using System.Collections.Generic;
using System.Linq;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public class TaxResponse
    {
        public TaxResponse()
        {
            // Orders = new List<OrderResponse>();
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

        // public virtual List<OrderResponse> Orders { get; set; }
    }
}
