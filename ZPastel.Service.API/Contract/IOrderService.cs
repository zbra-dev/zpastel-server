using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.API.Contract
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order createOrderCommand);
        Task<IReadOnlyList<Order>> FindAll();
        Task<Order> FindById(long id);
    }
}
