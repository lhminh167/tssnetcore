using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Models
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
