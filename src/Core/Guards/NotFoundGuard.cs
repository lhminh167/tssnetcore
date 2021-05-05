using Tss.Core.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class NotFoundGuard
    {
        public static void NotFound(this IGuardClause guardClause, object entity, string entityName)
        {
            if (entity == null)
                throw new NotFoundException(entityName);
        }
    }
}