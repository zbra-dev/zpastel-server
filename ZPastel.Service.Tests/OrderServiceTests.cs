using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using ZPastel.Model;
using ZPastel.Service.Contracts;
using ZPastel.Service.Impl;

namespace ZPastel.Service.Tests
{
    public class OrderServiceTests
    {
        private IOrderService orderService;
        public OrderServiceTests()
        {
            orderService = new OrderService();
        }

        [Fact]
        public void CreateOrder_FromPasteisOnly_ShouldCreateOrder()
        {
            var orderItems = new List<OrderItem>
            {
                new Pastel("Queijo", 7) { Quantity = 1 },
                new Pastel("Carne", 7) { Quantity = 2 },
                new Pastel("Calabresa", 7) { Quantity = 1 }
            };

            var order = orderService.CreateOrder(orderItems, 1);

            order.ClientId.Should().Be(1);
            order.DeliveryTax.Should().Be(0);
            order.OrderItems.Should().BeEquivalentTo(orderItems);
            order.TotalPrice.Should().Be(28);
        }
    }
}
