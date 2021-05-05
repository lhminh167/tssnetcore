using System.Net;
using Tss.Core.Exceptions;
using Tss.Core.Models;

namespace Ardalis.GuardClauses
{
    public static class UnsuccessfulPaymentGuard
    {
        public static void UnsuccessfulPayment(this IGuardClause guardClause, PaymentCreateResponse payment)
        {
            if (payment.Status != HttpStatusCode.OK)
                throw new BusinessException(payment);
        }
    }
}