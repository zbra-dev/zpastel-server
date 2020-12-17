using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        Task DeleteOrder(Order id);
        Task<IReadOnlyList<Order>> FindAll();
        Task<Order> FindById(long id);
        Task<IReadOnlyList<Order>> FindByUserId(long userId);
    }
}
