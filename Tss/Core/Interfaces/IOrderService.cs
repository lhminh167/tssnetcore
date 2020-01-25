using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IOrderService
    {
        Task<List<Orders>> Get();
        Task<Orders> Get(int orderId);
        Task<OrderCreateResponse> Create(OrderCreateRequest input);
        Task<Orders> StockOk(int orderId);
        Task<Orders> ShipOk(int orderId);
    }
}