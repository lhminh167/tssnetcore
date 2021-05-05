using System.Collections.Generic;
using Tss.Core.Entities;
using Tss.Core.Exceptions;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Ardalis.GuardClauses
{
    public static class EmailAddressTakenGuard
    {
        public static void EmailAddressTaken(this IGuardClause guardClause, Customer customer)
        {
            if (customer != null)
                throw new EmailAddressTakenException();
        }
    }
}