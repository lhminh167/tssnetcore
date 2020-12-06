using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Models
{
    public abstract class BaseProductSortByResponse : EnumerationResponse
    {
        protected BaseProductSortByResponse()
        {
        }
    }

    public class ProductSortByResponse : BaseProductSortByResponse
    {
        public ProductSortByResponse()
        {
        }
    }
}
