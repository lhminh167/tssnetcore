using System.Collections.Generic;
using System.Threading.Tasks;
using Tss.Core.Entities;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface IOrderService
    {
        Task<PaginatedList<Orders>> Get(PaginationRequest input);
        Task<Orders> Get(int orderId);
        Task<OrderCreateFormResponse> GetCreateForm();
        Task<OrderCreateResponse> CreatePayment(OrderCreateRequest input);
        Task<OrderCreateResponse> Create(OrderCreateRequest input);
        Task<Orders> StockOk(int orderId);
        Task<Orders> StockNotOk(int orderId, string comments);
        Task<Orders> ShipOk(int orderId);
    }
}