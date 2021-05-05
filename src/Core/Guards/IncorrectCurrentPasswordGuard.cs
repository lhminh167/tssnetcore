using Tss.Core.Entities;
using Tss.Core.Exceptions;
using Tss.Core.Models;

namespace Ardalis.GuardClauses
{
    public static class IncorrectCurrentPasswordGuard
    {
        public static void IncorrectCurrentPassword(this IGuardClause guardClause, string password, Customer customer)
        {
            if (customer == null || !customer.IsCorrectPassword(password))
                throw new IncorrectCurrentPasswordException();
        }
    }
}