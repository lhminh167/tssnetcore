using Ardalis.GuardClauses;
using Tss.Core.Models;

namespace Tss.Core.Models
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
