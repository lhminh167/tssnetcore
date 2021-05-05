using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.FunctionalTests.Core.Entities
{
    public abstract class BaseRole : Enumeration
    {
        protected BaseRole(int id, string message)
        : base(id, message)
        {
            Guard.Against.OutOfRange(id, nameof(id), 1, 3);
        }
    }

    public class Role : BaseRole
    {
        public static Role Customer = new Role(1, "Customer");
        public static Role Admin = new Role(3, "Admin");

        public Role(int id, string message)
        : base(id, message)
        {
        }
    }
}
