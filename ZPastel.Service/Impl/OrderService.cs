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
        private readonly OrderItemValidator orderItemValidator;
        private readonly UpdateOrderValidator updateOrderValidator;

        public OrderService(
            IOrderRepository orderRepository, 
            OrderValidator orderValidator,
            OrderItemValidator orderItemValidator,
            UpdateOrderValidator updateOrderValidator)
        {
            this.orderRepository = orderRepository;
            this.orderValidator = orderValidator;
            this.orderItemValidator = orderItemValidator;
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

        public async Task UpdateOrder(long id, UpdateOrder updateOrder)
        {
            await updateOrderValidator.Validate(updateOrder);

            var persistedOrder = await FindById(id);

            persistedOrder.TotalPrice = updateOrder.TotalPrice;
            persistedOrder.LastModifiedById = updateOrder.LastModifiedById;
            var now = DateTime.Now;
            persistedOrder.LastModifiedOn = now;
            var orderItemsNotToBeDeletedMapping = new Dictionary<long, OrderItem>();

            var persistedOrderItemsMapping = persistedOrder.OrderItems.ToDictionary(o => o.Id);
            foreach(var updatedOrderItem in updateOrder.UpdateOrderItems)
            {
                if (updatedOrderItem.Id != 0)
                {
                    orderItemsNotToBeDeletedMapping.Add(updatedOrderItem.Id, updatedOrderItem);
                }

                if (persistedOrderItemsMapping.ContainsKey(updatedOrderItem.Id))
                {
                    var persistedOrderItem = persistedOrderItemsMapping[updatedOrderItem.Id];
                    persistedOrderItem.Quantity = updatedOrderItem.Quantity;
                    persistedOrderItem.LastModifiedById = updatedOrderItem.LastModifiedById;
                    persistedOrderItem.LastModifiedOn = now;
                }
                else
                {
                    //TODO: check how to also validate when only updating.
                    updatedOrderItem.CreatedById = updateOrder.LastModifiedById;
                    updatedOrderItem.LastModifiedById = updateOrder.LastModifiedById;
                    updatedOrderItem.CreatedOn = now;
                    updatedOrderItem.LastModifiedOn = now;
                    await orderItemValidator.Validate(updatedOrderItem);
                    persistedOrder.OrderItems.Add(updatedOrderItem);
                }
            }

            var orderItemsTobeDeleted = new List<OrderItem>();
            foreach (var persistedOrderItemKeyValuePair in persistedOrderItemsMapping)
            {
                if (!orderItemsNotToBeDeletedMapping.ContainsKey(persistedOrderItemKeyValuePair.Key))
                {
                    orderItemsTobeDeleted.Add(persistedOrderItemKeyValuePair.Value);
                }
            }

            await orderRepository.UpdateOrder(persistedOrder, orderItemsTobeDeleted);
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
