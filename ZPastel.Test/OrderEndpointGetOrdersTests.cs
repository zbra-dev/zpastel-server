using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZPastel.API.Extensions;
using ZPastel.API.Resources;
using ZPastel.Tests;

namespace ZPastel.Test
{
    public class OrderEndpointGetOrdersTests
    {
        private readonly CustomWebApplicationFactory factory;
        private readonly HttpClient client;

        public OrderEndpointGetOrdersTests()
        {
            factory = new CustomWebApplicationFactory();
            client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Fact]
        public async Task GetOrders_AllOrders_ShouldReturnAllOrders()
        {
            var response = await client.GetAsync("api/orders");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var orders = await response.Deserialize<IReadOnlyCollection<OrderResource>>();

            orders.Count.Should().Be(2);

            var firstOrder = orders.First();

            firstOrder.Id.Should().Be(1);
            firstOrder.CreatedById.Should().Be(1);
            firstOrder.CreatedByUsername.Should().Be("Tester");
            firstOrder.LastModifiedById.Should().Be(1);
            firstOrder.TotalPrice.Should().Be(9.50m);

            var orderItemsFromFirstOrder = firstOrder.OrderItems;
            orderItemsFromFirstOrder.Count.Should().Be(2);

            var firstOrderItemFromFirstOrder = firstOrder.OrderItems.First();
            firstOrderItemFromFirstOrder.Id.Should().Be(1);
            firstOrderItemFromFirstOrder.CreatedById.Should().Be(1);
            firstOrderItemFromFirstOrder.Ingredients.Should().Be("Mussarela, Cheddar, Provolone, Catupiry");
            firstOrderItemFromFirstOrder.LastModifiedById.Should().Be(1);
            firstOrderItemFromFirstOrder.OrderId.Should().Be(1);
            firstOrderItemFromFirstOrder.PastelId.Should().Be(1);
            firstOrderItemFromFirstOrder.Price.Should().Be(5);
            firstOrderItemFromFirstOrder.Quantity.Should().Be(1);

            var secondOrderItemFromFirstOrder = firstOrder.OrderItems.Skip(1).First();
            secondOrderItemFromFirstOrder.Id.Should().Be(2);
            secondOrderItemFromFirstOrder.CreatedById.Should().Be(1);
            secondOrderItemFromFirstOrder.Ingredients.Should().Be("Carne Moida");
            secondOrderItemFromFirstOrder.LastModifiedById.Should().Be(1);
            secondOrderItemFromFirstOrder.OrderId.Should().Be(1);
            secondOrderItemFromFirstOrder.PastelId.Should().Be(2);
            secondOrderItemFromFirstOrder.Price.Should().Be(4.50m);
            secondOrderItemFromFirstOrder.Quantity.Should().Be(1);

            var secondOrder = orders.Skip(1).First();

            secondOrder.Id.Should().Be(2);
            secondOrder.CreatedById.Should().Be(1);
            secondOrder.CreatedByUsername.Should().Be("Tester");
            secondOrder.LastModifiedById.Should().Be(1);
            secondOrder.TotalPrice.Should().Be(18);

            var orderItemsFromSecondOrder = secondOrder.OrderItems;
            orderItemsFromSecondOrder.Count.Should().Be(1);

            var firstOrderItemFromSecondOrder = secondOrder.OrderItems.First();
            firstOrderItemFromSecondOrder.Id.Should().Be(3);
            firstOrderItemFromSecondOrder.CreatedById.Should().Be(1);
            firstOrderItemFromSecondOrder.Ingredients.Should().Be("Carne Moida");
            firstOrderItemFromSecondOrder.LastModifiedById.Should().Be(1);
            firstOrderItemFromSecondOrder.OrderId.Should().Be(1);
            firstOrderItemFromSecondOrder.PastelId.Should().Be(2);
            firstOrderItemFromSecondOrder.Price.Should().Be(4.50m);
            firstOrderItemFromSecondOrder.Quantity.Should().Be(4);
        }

        [Fact]
        public async Task GetOrderById_WithValidId_ShouldReturnCorrectOrder()
        {
            var response = await client.GetAsync("api/orders/2");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var order = await response.Deserialize<OrderResource>();

            order.Should().NotBeNull();
            order.Id.Should().Be(2);
            order.CreatedById.Should().Be(1);
            order.CreatedByUsername.Should().Be("Tester");
            order.LastModifiedById.Should().Be(1);
            order.TotalPrice.Should().Be(18);

            var orderItems = order.OrderItems;
            orderItems.Count.Should().Be(1);

            var firstOrderItemFromSecondOrder = order.OrderItems.First();
            firstOrderItemFromSecondOrder.Id.Should().Be(3);
            firstOrderItemFromSecondOrder.CreatedById.Should().Be(1);
            firstOrderItemFromSecondOrder.Ingredients.Should().Be("Carne Moida");
            firstOrderItemFromSecondOrder.LastModifiedById.Should().Be(1);
            firstOrderItemFromSecondOrder.OrderId.Should().Be(1);
            firstOrderItemFromSecondOrder.PastelId.Should().Be(2);
            firstOrderItemFromSecondOrder.Price.Should().Be(4.50m);
            firstOrderItemFromSecondOrder.Quantity.Should().Be(4);
        }

        [Fact]
        public async Task GetOrderById_WithInvalidId_ResponseStatusCodeShouldBeNotFound()
        {
            var response = await client.GetAsync("api/orders/0");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
