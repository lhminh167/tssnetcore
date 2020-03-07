using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IPaymentHelper
    {
        Task<PaymentCreateResponse> CreatePayment(Orders order);
        Task<PaymentCreateResponse> CompletePayment(string paymentId);

    }
}