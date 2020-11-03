using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Service.API.Repositories;

namespace ZPastel.Persistence.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            SetOrderToOrderItems(order);

            dataContext.Add(order);

            await dataContext.SaveChangesAsync();

            return order;
        }

        private Order SetOrderToOrderItems(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                orderItem.Order = order;
            }

            return order;
        }

        public async Task<IReadOnlyList<Order>> FindAll()
        {
            return await dataContext
                .Set<Order>()
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order> FindById(long id)
        {
            return await dataContext
                .Set<Order>()
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == id);
        }
    }
}
