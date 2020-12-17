using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Persistence.API.Repositories;
using ZPastel.Service.API.Contract;
using ZPastel.Service.Exceptions;
using ZPastel.Service.Validators;

namespace ZPastel.Service.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly OrderValidator orderValidator;

        public OrderService(IOrderRepository orderRepository, OrderValidator orderValidator)
        {
            this.orderRepository = orderRepository;
            this.orderValidator = orderValidator;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await orderValidator.Validate(order);

            var now = DateTime.Now;

            order.CreatedOn = now;
            order.LastModifiedOn = now;
            order.LastModifiedById = order.CreatedById;
            
            foreach (var orderItem in order.OrderItems)
            {
                orderItem.CreatedOn = now;
                orderItem.LastModifiedOn = now;
            }

            return await orderRepository.CreateOrder(order);
        }

        public async Task<IReadOnlyList<Order>> FindAll()
        {
            return await orderRepository.FindAll();
        }

        public async Task<Order> FindById(long id)
        {
            var order = await orderRepository.FindById(id);

            if (order == null)
            {
                throw new NotFoundException<Order>(id.ToString(), nameof(Order.Id));
            }

            return order;
        }
    }
}
