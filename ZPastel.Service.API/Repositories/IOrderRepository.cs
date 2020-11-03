using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.API.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        Task<IReadOnlyList<Order>> FindAll();
        Task<Order> FindById(long id);
    }
}
