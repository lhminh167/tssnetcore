using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Entities
{
    public abstract class BaseProductDisplay : Enumeration
    {
        protected BaseProductDisplay(int id, string message)
        : base(id, message)
        {
            Guard.Against.OutOfRange(id, nameof(id), 1, 2);
        }
    }

    public class ProductDisplay : BaseProductDisplay
    {
        public static ProductDisplay Other = new ProductDisplay(1, "Other");
        public static ProductDisplay HomepageTopPanel = new ProductDisplay(2, "HomepageTopPanel");

        public ProductDisplay(int id, string message)
        : base(id, message)
        {
        }
    }
}
