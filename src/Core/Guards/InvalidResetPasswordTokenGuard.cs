using Tss.Core.Entities;
using Tss.Core.Exceptions;
using Tss.Core.Models;

namespace Ardalis.GuardClauses
{
    public static class InvalidResetPasswordTokenGuard
    {
        public static void InvalidResetPasswordToken(this IGuardClause guardClause, bool isInvalid)
        {
            if (isInvalid)
                throw new InvalidResetPasswordTokenException();
        }
    }
}