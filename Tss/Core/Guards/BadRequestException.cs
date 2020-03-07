using Tss.Core.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class BadRequestGuard
    {
        public static void BadRequest(this IGuardClause guardClause, bool isBadRequest, string message)
        {
            if (isBadRequest)
                throw new BadRequestException(message);
        }
    }
}