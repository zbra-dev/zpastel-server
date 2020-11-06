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
        private readonly UpdateOrderValidator updateOrderValidator;

        public OrderService(
            IOrderRepository orderRepository, 
            OrderValidator orderValidator,
            UpdateOrderValidator updateOrderValidator)
        {
            this.orderRepository = orderRepository;
            this.orderValidator = orderValidator;
            this.updateOrderValidator = updateOrderValidator;
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

        public async Task UpdateOrder(long id, UpdateOrder updateOrderCommand)
        {
            await updateOrderValidator.Validate(updateOrderCommand);

            var order = await FindById(id);

            order.TotalPrice = updateOrderCommand.TotalPrice;
            order.LastModifiedById = updateOrderCommand.LastModifiedById;
            var now = DateTime.Now;
            order.LastModifiedOn = now;

            foreach(var updatedOrderItem in updateOrderCommand.UpdateOrderItems)
            {
                var orderItem = order.OrderItems.SingleOrDefault(o => o.Id == updatedOrderItem.Id);

                if (orderItem == null)
                {
                    //insert. See how to do this
                }

                orderItem.Quantity = updatedOrderItem.Quantity;
                orderItem.LastModifiedById = updatedOrderItem.ModifiedById;
                orderItem.LastModifiedOn = now;
            }

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
