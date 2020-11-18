﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Core.Repositories;
using ZPastel.Model;

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
            var createdOrder = dataContext.Add(order);
            await dataContext.SaveChangesAsync();

            return createdOrder.Entity;
        }

        private Order SetOrderToOrderItems(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                orderItem.Order = order;
            }

            return order;
        }

        public async Task UpdateOrder(Order order)
        {
            SetOrderToOrderItems(order);
            dataContext.Update(order);
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
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == id);
        }
    }
}
