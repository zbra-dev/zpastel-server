using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Service.Repositories;

namespace ZPastel.Persistence.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task CreateOrder(Order order)
        {
            dataContext.Add(order);

            await dataContext.SaveChangesAsync();
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
                .Where(o => o.Id == id)
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}
