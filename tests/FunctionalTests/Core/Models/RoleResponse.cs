using Ardalis.GuardClauses;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Models
{
    public abstract class BaseRoleResponse : EnumerationResponse
    {
        protected BaseRoleResponse()
        {
        }
    }

    public class RoleResponse : BaseRoleResponse
    {
        public RoleResponse()
        {
        }
    }
}
