using System.Threading.Tasks;
using Tss.Core.Entities;

namespace Tss.Core.Interfaces
{
    public interface IEmailHelper
    {
        Task SendOrderInitialNotification(Orders order);
        Task SendCheckStockNotification(Orders order);
        Task SendShipGoodsNotification(Orders order);
        Task SendOrderFinalNotification(Orders order);
        Task SendStockNotOkNotification(Orders order);
        Task SendInsufficientFundNotification(Orders order);
        Task SendResetPasswordEmail(Customer customer);
        Task SendPasswordChangeConfirmationEmail(Customer customer);
    }
}