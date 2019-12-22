using System.Threading.Tasks;
using Tss.Core.Entities;

namespace Tss.Core.Interfaces
{
    public interface IPaymentHelper
    {
        Task TakePaymentAsync(Orders order);
    }
}