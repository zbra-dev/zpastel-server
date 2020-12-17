using System;
using System.Collections.Generic;
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
        private readonly IUserRepository userRepository;
        private readonly OrderValidator orderValidator;

        public OrderService(
            IOrderRepository orderRepository,
            IUserRepository userRepository,
            OrderValidator orderValidator)
        {
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
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

        public async Task DeleteOrder(long id)
        {
            var order = await orderRepository.FindById(id);

            if (order == null)
            {
                throw new NotFoundException<Order>(id.ToString(), nameof(Order.Id));
            }

            await orderRepository.DeleteOrder(order);
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

        public async Task<IReadOnlyList<Order>> FindByUserId(long userId)
        {
            var user = await userRepository.FindById(userId);

            if (user == null)
            {
                throw new NotFoundException<User>(userId.ToString(), nameof(User.Id));
            }

            return await orderRepository.FindByUserId(userId);
        }
    }
}
