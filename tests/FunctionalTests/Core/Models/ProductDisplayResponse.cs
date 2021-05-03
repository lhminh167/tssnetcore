using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public abstract class BaseProductDisplayResponse : EnumerationResponse
    {
        protected BaseProductDisplayResponse()
        {
        }
    }

    public class ProductDisplayResponse : BaseProductDisplayResponse
    {
        public ProductDisplayResponse()
        {
        }
    }
}
