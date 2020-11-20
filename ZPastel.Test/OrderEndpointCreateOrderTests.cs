using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
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
using ZPastel.Test.Builders;
using ZPastel.Test.Extensions;
using ZPastel.Tests;

namespace ZPastel.Test
{
    public class OrderEndpointCreateOrderTests
    {
        private readonly CustomWebApplicationFactory factory;
        private readonly HttpClient client;

        public OrderEndpointCreateOrderTests()
        {
            factory = new CustomWebApplicationFactory();
            client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Fact]
        public async Task CreateOrder_WithValidOrderResource_ShouldCreateOrder()
        {
            var body = new OrderResourceBuilder()
                .WithDefaultValues()
                .Build();

            var getResponse = await client.GetAsync("api/orders");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var orders = await getResponse.Deserialize<IReadOnlyCollection<OrderResource>>();

            orders.Count.Should().Be(2);

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdOrder = await postResponse.Deserialize<OrderResource>();

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

            orders = await getResponse.Deserialize<IReadOnlyCollection<OrderResource>>();

            orders.Count.Should().Be(3);
        }

        [Fact]
        public async Task CreateOrder_WithUserIdNotFound_ResponseStatusCodeShouldBeNotFound()
        {
            var body = new OrderResourceBuilder()
               .WithDefaultValues()
               .WithCreatedById(3)
               .Build();

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

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

            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateOrder_WithUserIdNotFoundInOrderItem_ResponseStatusCodeShouldBeNotFound()
        {
            var orderItemResource = new OrderItemResource
            {
                CreatedById = 3,
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

            var postResponse = await client.PostAsync("api/orders/create", content);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
