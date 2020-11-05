using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Core.Repositories;
using ZPastel.Model;
using ZPastel.Service.Contract;
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

        public async Task UpdateOrder(Order updateOrderCommand)
        {
            var id = updateOrderCommand.Id;
            var order = await FindById(id);

            if (order == null)
            {
                throw new NotFoundException<Order>(id.ToString(), nameof(Order.Id));
            }

            order.TotalPrice = updateOrderCommand.TotalPrice;
            order.LastModifiedById = updateOrderCommand.LastModifiedById;
            var now = DateTime.Now;
            order.LastModifiedOn = now;

            foreach(var updatedOrderItem in updateOrderCommand.OrderItems)
            {
                if (!order.OrderItems.Any(o => o.Id == updatedOrderItem.Id))
                {
                    //TODO: create a better exception for this
                    throw new NotFoundException<OrderItem>(updatedOrderItem.Id.ToString(), nameof(OrderItem.Id));
                }

                var orderItem = order.OrderItems.Single(o => o.Id == updatedOrderItem.Id);
                orderItem.Name = updatedOrderItem.Name;
                orderItem.Price = updatedOrderItem.Price;
                orderItem.Quantity = updatedOrderItem.Quantity;
                orderItem.Ingredients = updatedOrderItem.Ingredients;
                orderItem.LastModifiedById = updatedOrderItem.LastModifiedById;
                orderItem.LastModifiedOn = now;
            }
            await orderValidator.Validate(order);

            await orderRepository.UpdateOrder(order);
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
