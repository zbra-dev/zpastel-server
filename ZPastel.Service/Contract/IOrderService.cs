using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.Contract
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order createOrderCommand);
        Task DeleteOrder(long id);
        Task<IReadOnlyList<Order>> FindAll();
        Task<Order> FindById(long id);

        Task<IReadOnlyList<Order>> FindByUserId(long userId);
    }
}
