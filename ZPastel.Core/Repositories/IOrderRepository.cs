using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        Task UpdateOrder(Order order, IList<OrderItem> orderItemsToBeDeleted);
        Task<IReadOnlyList<Order>> FindAll();
        Task<Order> FindById(long id);
    }
}
