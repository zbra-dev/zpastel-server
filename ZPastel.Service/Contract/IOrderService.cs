using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.Contract
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task UpdateOrder(long id, UpdateOrder updateOrder);
        Task<IReadOnlyList<Order>> FindAll();
        Task<Order> FindById(long id);
    }
}
