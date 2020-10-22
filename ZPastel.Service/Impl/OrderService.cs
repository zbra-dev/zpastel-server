﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Service.API.Repositories;
using ZPastel.Service.Contract;
using ZPastel.Service.Exceptions;
using ZPastel.Service.Validators;

namespace ZPastel.Service.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly OrderValidator createOrderCommandValidator;

        public OrderService(IOrderRepository orderRepository, OrderValidator createOrderCommandValidator)
        {
            this.orderRepository = orderRepository;
            this.createOrderCommandValidator = createOrderCommandValidator;
        }

        public async Task CreateOrder(Order order)
        {
            await createOrderCommandValidator.Validate(order);

            var now = DateTime.Now;

            order.CreatedOn = now;
            order.LastModifiedOn = now;
            order.LastModifiedById = order.CreatedById;
            
            foreach (var orderItem in order.OrderItems)
            {
                orderItem.CreatedOn = now;
                orderItem.LastModifiedOn = now;
            }

            await orderRepository.CreateOrder(order);
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
