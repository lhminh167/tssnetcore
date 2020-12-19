using Tss.Core.Entities;
using Tss.Core.Exceptions;
using Tss.Core.Models;

namespace Ardalis.GuardClauses
{
    public static class IncorrectLoginInfoGuard
    {
        public static void IncorrectLoginInfo(this IGuardClause guardClause, LoginRequest input, Customer customer)
        {
            if (customer == null || !customer.IsCorrectPassword(input.Password))
                throw new IncorrectLoginInfoException();
        }
    }
}