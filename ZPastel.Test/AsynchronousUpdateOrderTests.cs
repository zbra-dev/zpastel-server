using FluentAssertions;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZPastel.Core.Repositories;
using ZPastel.Model;
using ZPastel.Persistence.Impl;
using ZPastel.Service.Impl;
using ZPastel.Service.Validators;
using ZPastel.Test.Converters;

namespace ZPastel.Test
{
    public class AsynchronousUpdateOrderTests
    {
        private readonly IOrderRepository orderRepository;
        private readonly IPastelRepository pastelRepository;
        private readonly IUserRepository userRepository;
        private readonly UpdateOrderConverter updateOrderConverter;
        private readonly OrderValidator orderValidator;
        private readonly OrderItemValidator orderItemValidator;
        private readonly UpdateOrderValidator updateOrderValidator;

        public AsynchronousUpdateOrderTests()
        {
            var dataContext = new DataContextFactory().CreateSeededDataContext();
            orderRepository = new OrderRepository(dataContext);
            pastelRepository = new PastelRepository(dataContext);
            userRepository = new UserRepository(dataContext);
            updateOrderConverter = new UpdateOrderConverter();
            updateOrderValidator = new UpdateOrderValidator(userRepository);
            orderItemValidator = new OrderItemValidator(pastelRepository, userRepository);
            orderValidator = new OrderValidator(orderItemValidator, userRepository);
        }

        [Fact]
        public void UpdateOrder_WithMultipleTasks_ShouldUpdateOrderConsistently()
        {
            var numberOfRunningTasks = 25;
            var runningTasks = new ConcurrentBag<Task>();

            for (var i = 0; i < numberOfRunningTasks; i++)
            {
                runningTasks.Add(Task.Run(() =>
                {
                    var orderService = new OrderService(
                            orderRepository,
                            orderValidator,
                            orderItemValidator,
                            updateOrderValidator);

                    for (var j = 0; j < 100; j++)
                    {
                        var order = orderRepository.FindById(1).Result;
                        UpdateOrderItem(order.OrderItems.FirstOrDefault());
                        PersistOrder(1, order, orderService).RunSynchronously();

                        order = orderRepository.FindById(1).Result;
                        AddOrderItemToOrder(order).RunSynchronously();
                        PersistOrder(1, order, orderService).RunSynchronously();

                        order = orderRepository.FindById(1).Result;
                        if (order.OrderItems.Any())
                        {
                            RemoveOrderItemFromOrder(order.OrderItems.First(), order);
                        }
                        PersistOrder(1, order, orderService).RunSynchronously();
                    }
                }));
            }

            Task.WaitAll(runningTasks.ToArray());

            foreach (var task in runningTasks)
            {
                task.Status.Should().Be(TaskStatus.RanToCompletion);
            }
        }

        private async Task PersistOrder(long id, Order order, OrderService service)
        {
            var orderToBeUpdated = updateOrderConverter.ConvertToUpdateOrder(order);
            await service.UpdateOrder(id, orderToBeUpdated);
        }

        private async Task AddOrderItemToOrder(Order order)
        {
            var pastelCamarao = await pastelRepository.FindById(3);

            order.OrderItems.Add(new OrderItem()
            {
                Ingredients = pastelCamarao.Ingredients,
                Name = pastelCamarao.Name,
                PastelId = pastelCamarao.Id,
                Price = pastelCamarao.Price,
                Quantity = 2
            });
        }

        private void UpdateOrderItem(OrderItem orderItem)
        {
            if (orderItem != null)
            {
                orderItem.Quantity = 2;
            }
        }

        private void RemoveOrderItemFromOrder(OrderItem orderItem, Order order)
        {
            var index = order.OrderItems.IndexOf(orderItem);

            if (index >= 0)
            {
                order.OrderItems.RemoveAt(index);
            }
        }
    }
}
