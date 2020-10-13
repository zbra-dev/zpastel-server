using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.Contract
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderCommand createOrderCommand);
        Task<IReadOnlyList<Order>> FindAll();
        Task<Order> FindById(long id);
    }
}
