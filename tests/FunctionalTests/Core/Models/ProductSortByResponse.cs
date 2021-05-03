using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
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
