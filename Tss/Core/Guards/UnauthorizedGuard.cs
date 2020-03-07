using Tss.Core.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class UnauthorizedGuard
    {
        public static void Unauthorized(this IGuardClause guardClause, bool isUnauthorized)
        {
            if (isUnauthorized)
                throw new UnauthorizedException();
        }
    }
}