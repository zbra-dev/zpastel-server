using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Service.Contract;
using ZPastel.Service.Exceptions;
using ZPastel.Service.Repositories;
using ZPastel.Service.Validators;

namespace ZPastel.Service.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly CreateOrderCommandValidator createOrderCommandValidator;

        public OrderService(IOrderRepository orderRepository, CreateOrderCommandValidator createOrderCommandValidator)
        {
            this.orderRepository = orderRepository;
            this.createOrderCommandValidator = createOrderCommandValidator;
        }

        public async Task CreateOrder(CreateOrderCommand createOrderCommand)
        {
            await createOrderCommandValidator.Validate(createOrderCommand);

            var order = CreateOrderFromCommand(createOrderCommand);

            await orderRepository.CreateOrder(order);
        }

        private Order CreateOrderFromCommand(CreateOrderCommand createOrderCommand)
        {
            var order = new Order
            {
                CreatedById = createOrderCommand.CreatedById,
                CreatedByUsername = createOrderCommand.CreatedByUserName,
                CreatedOn = DateTime.Now,
                LastModifiedById = createOrderCommand.CreatedById,
                LastModifiedOn = DateTime.Now,
                TotalPrice = CalculateTotalPrice(createOrderCommand.OrderItems)
            };
            order.OrderItems = CreateOrderItems(createOrderCommand.OrderItems, order);

            return order;
        }

        private IList<OrderItem> CreateOrderItems(IList<CreateOrderItem> createOrderItems, Order order)
        {
            return createOrderItems
                .Select(c => new OrderItem
                {
                    Order = order,
                    CreatedById = c.CreatedById,
                    CreatedOn = DateTime.Now,
                    Ingredients = c.Ingredients,
                    LastModifiedById = c.CreatedById,
                    LastModifiedOn = DateTime.Now,
                    Name = c.Name,
                    PastelId = c.PastelId,
                    Price = c.Price,
                    Quantity = c.Quantity
                })
                .ToList();
        }

        private decimal CalculateTotalPrice(IList<CreateOrderItem> orderItems)
        {
            return orderItems.Sum(o => o.Price * o.Quantity);
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
