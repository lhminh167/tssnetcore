using Tss.Core.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class AlreadyExistGuard
    {
        public static void AlreadyExist(this IGuardClause guardClause, object entity, string entityName)
        {
            if (entity != null)
                throw new AlreadyExistException(entityName);
        }
    }
}