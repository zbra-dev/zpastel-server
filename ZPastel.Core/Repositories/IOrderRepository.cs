using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Core.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task<IReadOnlyList<Order>> FindAll();
        Task<Order> FindById(long id);
    }
}
