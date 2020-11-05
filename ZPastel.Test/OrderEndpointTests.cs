using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using ZPastel.API.Resources;
using ZPastel.Model;
using ZPastel.Persistence;
using ZPastel.Test.Builders;
using ZPastel.Tests;

namespace ZPastel.Test
{
    public class OrderEndpointTests : IDisposable
    {
        private readonly CustomWebApplicationFactory factory;
        private readonly DataContext dataContext;
        private readonly IServiceScope serviceScope;
        public OrderEndpointTests()
        {
            factory = new CustomWebApplicationFactory();

            serviceScope = factory.Services.CreateScope();

            dataContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        }

        public void Dispose()
        {
            serviceScope.Dispose();
        }

        [Fact]
        public async Task GetOrders_AllOrders_ShouldReturnAllOrders()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/orders");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ordersContent = await response.Content.ReadAsStringAsync();
            var orders = Newtonsoft.Json.JsonConvert.DeserializeObject<IReadOnlyCollection<OrderResource>>(ordersContent);

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
            firstOrderItemFromFirstOrder.OrderId.Should().Be(firstOrder.Id);
            firstOrderItemFromFirstOrder.PastelId.Should().Be(1);
            firstOrderItemFromFirstOrder.Price.Should().Be(5);
            firstOrderItemFromFirstOrder.Quantity.Should().Be(1);

            var secondOrderItemFromFirstOrder = firstOrder.OrderItems.Skip(1).First();
            secondOrderItemFromFirstOrder.Id.Should().Be(2);
            secondOrderItemFromFirstOrder.CreatedById.Should().Be(1);
            secondOrderItemFromFirstOrder.Ingredients.Should().Be("Carne Moida");
            secondOrderItemFromFirstOrder.LastModifiedById.Should().Be(1);
            secondOrderItemFromFirstOrder.OrderId.Should().Be(firstOrder.Id);
            secondOrderItemFromFirstOrder.PastelId.Should().Be(2);
            secondOrderItemFromFirstOrder.Price.Should().Be(4.50m);
            secondOrderItemFromFirstOrder.Quantity.Should().Be(1);

            var secondOrder = orders.Skip(1).First();

            secondOrder.Id.Should().Be(2);
            secondOrder.CreatedById.Should().Be(2);
            secondOrder.CreatedByUsername.Should().Be("Tester 2");
            secondOrder.LastModifiedById.Should().Be(2);
            secondOrder.TotalPrice.Should().Be(18);

            var orderItemsFromSecondOrder = secondOrder.OrderItems;
            orderItemsFromSecondOrder.Count.Should().Be(1);

            var firstOrderItemFromSecondOrder = secondOrder.OrderItems.First();
            firstOrderItemFromSecondOrder.Id.Should().Be(3);
            firstOrderItemFromSecondOrder.CreatedById.Should().Be(2);
            firstOrderItemFromSecondOrder.Ingredients.Should().Be("Carne Moida");
            firstOrderItemFromSecondOrder.LastModifiedById.Should().Be(2);
            firstOrderItemFromSecondOrder.OrderId.Should().Be(secondOrder.Id);
            firstOrderItemFromSecondOrder.PastelId.Should().Be(2);
            firstOrderItemFromSecondOrder.Price.Should().Be(4.50m);
            firstOrderItemFromSecondOrder.Quantity.Should().Be(4);
        }

        private HttpClient GetClient()
        {
            return factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Fact]
        public async Task GetOrderById_WithValidId_ShouldReturnCorrectOrder()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/orders/2");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ordersContent = await response.Content.ReadAsStringAsync();
            var order = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderResource>(ordersContent);

            order.Should().NotBeNull();
            order.Id.Should().Be(2);
            order.CreatedById.Should().Be(2);
            order.CreatedByUsername.Should().Be("Tester 2");
            order.LastModifiedById.Should().Be(2);
            order.TotalPrice.Should().Be(18);

            var orderItems = order.OrderItems;
            orderItems.Count.Should().Be(1);

            var firstOrderItemFromSecondOrder = order.OrderItems.First();
            firstOrderItemFromSecondOrder.Id.Should().Be(3);
            firstOrderItemFromSecondOrder.CreatedById.Should().Be(2);
            firstOrderItemFromSecondOrder.Ingredients.Should().Be("Carne Moida");
            firstOrderItemFromSecondOrder.LastModifiedById.Should().Be(2);
            firstOrderItemFromSecondOrder.OrderId.Should().Be(order.Id);
            firstOrderItemFromSecondOrder.PastelId.Should().Be(2);
            firstOrderItemFromSecondOrder.Price.Should().Be(4.50m);
            firstOrderItemFromSecondOrder.Quantity.Should().Be(4);
        }

        [Fact]
        public async Task GetOrderById_WithInvalidId_ResponseStatusCodeShouldBeNotFound()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/orders/0");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateOrder_WithValidOrderResource_ShouldCreateOrder()
        {
            var body = new OrderResourceBuilder()
                .WithDefaultValues()
                .Build();

            var client = GetClient();
            var getResponse = await client.GetAsync("api/orders");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var ordersContent = await getResponse.Content.ReadAsStringAsync();
            var orders = Newtonsoft.Json.JsonConvert.DeserializeObject<IReadOnlyCollection<OrderResource>>(ordersContent);

            orders.Count.Should().Be(2);

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdOrderResponse = await postResponse.Content.ReadAsStringAsync();
            var createdOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderResource>(createdOrderResponse);

            createdOrder.CreatedById.Should().Be(body.CreatedById);
            createdOrder.CreatedByUsername.Should().Be(body.CreatedByUsername);
            createdOrder.CreatedOn.Should().BeAfter(DateTime.MinValue);
            createdOrder.LastModifiedById.Should().Be(body.CreatedById);
            createdOrder.LastModifiedOn.Should().BeAfter(DateTime.MinValue);
            createdOrder.TotalPrice.Should().Be(body.TotalPrice);
            createdOrder.OrderItems.Count.Should().Be(body.OrderItems.Count);
            createdOrder.Id.Should().BeGreaterThan(0);

            var orderItemFromCreatedOrder = createdOrder.OrderItems.First();
            var orderItemFromBody = body.OrderItems.First();

            orderItemFromCreatedOrder.CreatedById.Should().Be(orderItemFromBody.CreatedById);
            orderItemFromCreatedOrder.Ingredients.Should().Be(orderItemFromBody.Ingredients);
            orderItemFromCreatedOrder.PastelId.Should().Be(orderItemFromBody.PastelId);
            orderItemFromCreatedOrder.Price.Should().Be(orderItemFromBody.Price);
            orderItemFromCreatedOrder.Quantity.Should().Be(orderItemFromBody.Quantity);
            orderItemFromCreatedOrder.Name.Should().Be(orderItemFromBody.Name);
            orderItemFromCreatedOrder.OrderId.Should().Be(createdOrder.Id);
            orderItemFromCreatedOrder.Id.Should().BeGreaterThan(0);

            getResponse = await client.GetAsync("api/orders");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            ordersContent = await getResponse.Content.ReadAsStringAsync();
            orders = Newtonsoft.Json.JsonConvert.DeserializeObject<IReadOnlyCollection<OrderResource>>(ordersContent);

            orders.Count.Should().Be(3);
        }

        [Fact]
        public async Task CreateOrder_WithUserIdNotFound_ResponseStatusCodeShouldBeNotFound()
        {
            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithCreatedById(200)
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateOrder_WithNegativeTotalPrice_ResponseStatusCodeShouldBeBadRequest()
        {
            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithTotalPrice(-2)
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateOrder_WithEmptyCreatedByUsername_ResponseStatusCodeShouldBeBadRequest()
        {
            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithCreatedByUsername(string.Empty)
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateOrder_WithEmptyOrderItemsList_ResponseStatusCodeShouldBeBadRequest()
        {
            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithOrderItems(new List<OrderItemResource>())
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateOrder_WithUserIdNotFoundInOrderItem_ResponseStatusCodeShouldBeNotFound()
        {
            var orderItemResource = new OrderItemResource
            {
                CreatedById = 200,
                Ingredients = "Quejo",
                Name = "Pastel de Queijo",
                PastelId = 1,
                Price = 4,
                Quantity = 1
            };

            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithOrderItems(new[] { orderItemResource })
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateOrder_WithEmptyIngredientsInOrderItem_ResponseStatusCodeShouldBeBadRequest()
        {
            var orderItemResource = new OrderItemResource
            {
                CreatedById = 1,
                Ingredients = string.Empty,
                Name = "Pastel de Queijo",
                PastelId = 1,
                Price = 4,
                Quantity = 1
            };

            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithOrderItems(new[] { orderItemResource })
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateOrder_WithEmptyNameInOrderItem_ResponseStatusCodeShouldBeBadRequest()
        {
            var orderItemResource = new OrderItemResource
            {
                CreatedById = 1,
                Ingredients = "Queijo",
                Name = string.Empty,
                PastelId = 1,
                Price = 4,
                Quantity = 1
            };

            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithOrderItems(new[] { orderItemResource })
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateOrder_WithPastelIdNotFoundInOrderItem_ResponseStatusCodeShouldBeNotFound()
        {
            var orderItemResource = new OrderItemResource
            {
                CreatedById = 1,
                Ingredients = "Queijo",
                Name = "Pastel de Queijo",
                PastelId = 100,
                Price = 4,
                Quantity = 1
            };

            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithOrderItems(new[] { orderItemResource })
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateOrder_WithNegativePriceInOrderItem_ResponseStatusCodeShouldBeBadRequest()
        {
            var orderItemResource = new OrderItemResource
            {
                CreatedById = 1,
                Ingredients = "Queijo",
                Name = "Pastel de Queijo",
                PastelId = 1,
                Price = -4,
                Quantity = 1
            };

            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithOrderItems(new[] { orderItemResource })
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task CreateOrder_WithNegativeAndZeroQuantityInOrderItem_ResponseStatusCodeShouldBeBadRequest(int quantity)
        {
            var orderItemResource = new OrderItemResource
            {
                CreatedById = 1,
                Ingredients = "Queijo",
                Name = "Pastel de Queijo",
                PastelId = 1,
                Price = 4,
                Quantity = quantity
            };

            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithOrderItems(new[] { orderItemResource })
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var client = GetClient();
            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteOrder_WithOrderItems_ShouldDeleteTheOrderAndAllOrderItems()
        {
            var order = dataContext
                .Set<Order>()
                .Where(o => o.Id == 2)
                .SingleOrDefault();

            order.Should().NotBeNull();

            var client = GetClient();
            var response = await client.DeleteAsync("api/orders/delete/2");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            order = dataContext
                .Set<Order>()
                .Where(o => o.Id == 2)
                .SingleOrDefault();

            order.Should().BeNull();

            var orderItems = dataContext
                .Set<OrderItem>()
                .Where(o => o.OrderId == 2)
                .ToList();

            orderItems.Should().BeEmpty();
        }

        [Fact]
        public async Task GetOrders_ForUserInTheDatabase_ShouldReturnAllOrdersCreatedByThisUser()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/orders/user/2");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ordersContent = await response.Content.ReadAsStringAsync();
            var orders = Newtonsoft.Json.JsonConvert.DeserializeObject<IReadOnlyCollection<OrderResource>>(ordersContent);

            orders.Count.Should().Be(1);

            var userOrder = orders.First();

            userOrder.Id.Should().Be(2);
            userOrder.CreatedById.Should().Be(2);
            userOrder.CreatedByUsername.Should().Be("Tester 2");
            userOrder.LastModifiedById.Should().Be(2);
            userOrder.TotalPrice.Should().Be(18);

            var orderItemsFromUserOrder = userOrder.OrderItems;
            orderItemsFromUserOrder.Count.Should().Be(1);

            var firstOrderItemFromUserOrder = userOrder.OrderItems.First();
            firstOrderItemFromUserOrder.Id.Should().Be(3);
            firstOrderItemFromUserOrder.CreatedById.Should().Be(2);
            firstOrderItemFromUserOrder.Ingredients.Should().Be("Carne Moida");
            firstOrderItemFromUserOrder.LastModifiedById.Should().Be(2);
            firstOrderItemFromUserOrder.OrderId.Should().Be(userOrder.Id);
            firstOrderItemFromUserOrder.PastelId.Should().Be(2);
            firstOrderItemFromUserOrder.Price.Should().Be(4.50m);
            firstOrderItemFromUserOrder.Quantity.Should().Be(4);
        }

        [Fact]
        public async Task GetOrders_ForUserNotFound_ShouldReturnNotFound()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/orders/user/200");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
